using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.ServiceInterface;
using Shop.Core.Dto;
using System.Text.Json;
namespace Shop.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto)
        {
            string apiKey = "";
            var response = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={dto.CityName}";

            using (var client = new HttpClient())
            {
                var httpResponse = await client.GetAsync(response);
                string json = await httpResponse.Content.ReadAsStringAsync();

                List<AccuCityCodeRootDto> weatherData =
                     JsonSerializer.Deserialize<List<AccuCityCodeRootDto>>(json);

                dto.CityName = weatherData[0].LocalizedName;
                dto.CityCode = weatherData[0].Key;
            }

            string weatherResponse = $"http://dataservice.accuweather.com/currentconditions/v1/{dto.CityCode}?apikey={apiKey}";

            using (var clientWeather = new HttpClient())
            {
                var httpResponseWeather = await clientWeather.GetAsync(weatherResponse);
                string jsonWeather = await httpResponseWeather.Content.ReadAsStringAsync();

                List<AccuLocationRootDto> weatherDataResult =
                     JsonSerializer.Deserialize<List<AccuLocationRootDto>>(jsonWeather);

                dto.WeatherText = weatherDataResult[0].WeatherText;
                dto.MobileLink = weatherDataResult[0].MobileLink;
                dto.Link = weatherDataResult[0].Link;
                dto.EpochTime = weatherDataResult[0].EpochTime;
                dto.LocalObservationDateTime = weatherDataResult[0].LocalObservationDateTime;
                dto.TemperatureMetricValue = weatherDataResult[0].Temperature.Metric.Value;

            }

                return dto;

        }
    }
}
