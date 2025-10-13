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
            var response = $"https://api.weatherapi.com/v1/current.json";

            using (var client = new HttpClient())
            {
                var httpResponse = await client.GetAsync(response);
                string json = await httpResponse.Content.ReadAsStringAsync();

                List<AccuLocationWeatherResultDto> weatherData =
                    JsonSerializer.Deserialize<List<AccuLocationWeatherResultDto>>(json);
            }
            return dto;

        }
    }
}
