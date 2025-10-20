
namespace Shop.Core.Dto
{
    public class AccuLocationWeatherResultDto
    {
        public string CityName { get; set; } = string.Empty;
        public string CityCode { get; set; } = string.Empty;
        public string LocalObservationDateTime { get; set; } = string.Empty;
        public int EpochTime { get; set; }
        public string WeatherText { get; set; } = string.Empty;
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; } = string.Empty;
        public bool IsDayTime { get; set; }

        public string TemperatureMetricUnit { get; set; } = string.Empty;
        public double TemperatureMetricValue { get; set; }
        public int TemperatureMetricUnitType { get; set; }

        public string TemperatureImperialUnit { get; set; } = string.Empty;
        public double TemperatureImperialValue { get; set; }
        public int TemperatureImperialUnitType { get; set; }

        public string MobileLink { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
