using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
       
       // public string ClientIPAddr { get; private set; }

        [HttpGet]
        public WeatherForecastModel GetWeatherForecast()
        {
            WebClient client = new WebClient();
            WeatherForecastModel Curlist = new WeatherForecastModel();

            using (var reader = new DatabaseReader(@"C:\myRepository\WeatherForecast\db\GeoLite2-City.mmdb"))
            {
                // Determine the IP Address of the request
                //var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                var serializedIp = JsonConvert.DeserializeObject<IpAddressModel>(client.DownloadString("https://api.ipify.org/?format=json"));
                // Get the city from the IP Address
                if (serializedIp != null) {
                    var city = reader.City(serializedIp.ip).City.ToString();
                    var json = client.DownloadString($"https://api.openweathermap.org/data/2.5/weather?q={city}&lang=tr&appid=f88bcf7a60b5f5f161be24945d7b758f");
                    if(json != null)
                    {
                        Curlist = JsonConvert.DeserializeObject<WeatherForecastModel>(json);
                        Curlist.Main.Temp = Math.Floor(Curlist.Main.Temp) - 273;
                    }
                }         
            }        
            return Curlist;
        }
    }
}
