namespace Shop.Models.OpenWeather
{
    public class OpenWeatherViewModel
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public double TempFeelsLike { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public double WindSpeed { get; set; }
        public string WeatherCondition { get; set; }
    }
}
