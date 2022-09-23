namespace WeatherForecast.Models
{
    public class WeatherForecastModel
    {
        //public int Coord { get; set; }
        public  List<WeatherDetailModel> Weather { get; set; }
        //public string Base { get; set; }
       public TempDetails Main { get; set; }
        public int Visibility { get; set; }
        public WindDetail Wind { get; set; }
        //public int Rain { get; set; }
        //public int Clouds { get; set; }
        //public int Dt { get; set; }
        //public int Sys { get; set; }
        public string Timezone { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }


    }
}
