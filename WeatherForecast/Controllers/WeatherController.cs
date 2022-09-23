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
       
        public string ClientIPAddr { get; private set; }

        public WeatherController()
        {
        
        }
    //    private static readonly string[] Summaries = new[]
    //    {
    //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //};

    //    private readonly ILogger<WeatherForecastController> _logger;

    //    public WeatherController(ILogger<WeatherForecastController> logger)
    //    {
    //        _logger = logger;
    //    }

    //    [HttpGet(Name = "GetWeatherForecast")]
    //    public IEnumerable<WeatherForecast> Get()
    //    {
    //        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //        {
    //            Date = DateTime.Now.AddDays(index),
    //            TemperatureC = Random.Shared.Next(-20, 55),
    //            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //        })
    //        .ToArray();
    //    }

        [HttpGet]
        public WeatherForecastModel GetWeatherForecast()
        {
            WebClient client = new WebClient();
            WeatherForecastModel Curlist = new WeatherForecastModel();

            using (var reader = new DatabaseReader(@"C:\myRepository\WeatherForecast\db\GeoLite2-City.mmdb"))
            {
                // Determine the IP Address of the request
                //var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                var ipAddress = client.DownloadString("https://api.ipify.org/?format=json");
                var serializeIp = JsonConvert.DeserializeObject<IpAddressModel>(ipAddress);
                // Get the city from the IP Address
                var city = reader.City(serializeIp.ip).City.ToString();
                var json = client.DownloadString($"https://api.openweathermap.org/data/2.5/weather?q={city}&lang=tr&appid=f88bcf7a60b5f5f161be24945d7b758f");
                Curlist = JsonConvert.DeserializeObject<WeatherForecastModel>(json);
            }

            Curlist.Main.Temp = Math.Floor(Curlist.Main.Temp) - 273;
            return Curlist;
        }
    }
}
