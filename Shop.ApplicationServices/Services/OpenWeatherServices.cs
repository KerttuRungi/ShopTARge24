using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;

namespace Shop.ApplicationServices.Services
{
    public class OpenWeatherServices : IOpenWeatherServices
    {
        private readonly HttpClient _httpClient;

        public OpenWeatherServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OpenWeatherResultDto> OpenWeatherResultDto(OpenWeatherResultDto dto)
        {

            string apiKey = "5420c90e098df4fd16e8cd8af55b3c10"; 
            string baseUrl = "https://api.openweathermap.org/data/2.5/weather";
            string url = $"{baseUrl}?q={dto.City}&appid={apiKey}&units=metric";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync(url);
                var jsonResponse = await response.Content.ReadAsStringAsync();

               
                var weatherData = JsonConvert.DeserializeObject<OpenWeatherRootDto>(jsonResponse);

               
                dto.CityName = weatherData.name;
                dto.Temperature = weatherData.main.temp;
                dto.FeelsLike = weatherData.main.feels_like;
                dto.Humidity = weatherData.main.humidity;
                dto.Pressure = weatherData.main.pressure;
                dto.WindSpeed = weatherData.wind.speed;
                dto.WeatherCondition = weatherData.weather[0].main;
            }

            return dto;
        }
    }
}
