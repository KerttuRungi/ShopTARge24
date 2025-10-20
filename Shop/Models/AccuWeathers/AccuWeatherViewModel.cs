namespace Shop.Models.AccuWeathers
{
    public class AccuWeatherViewModel
    {
        public string CityName { get; set; } = string.Empty;
        public string EffectiveDate { get; set; } = string.Empty;
        public int Severity { get; set; }
        public string Category { get; set; } = string.Empty;
        public string WeatherText { get; set; } = string.Empty;
        public double TempMaxCelsius { get; set; }
        public double TempMinCelsius { get; set; }
    }
}
