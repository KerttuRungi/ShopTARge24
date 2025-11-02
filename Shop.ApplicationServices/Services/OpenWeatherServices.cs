// OpenWeatherServices.cs
using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
            if (string.IsNullOrWhiteSpace(dto.City))
                throw new Exception("City name cannot be empty");

            string apiKey = "5420c90e098df4fd16e8cd8af55b3c10";
            string baseUrl = "https://api.openweathermap.org/data/2.5/weather";
            string url = $"{baseUrl}?q={Uri.EscapeDataString(dto.City)}&appid={apiKey}&units=metric";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"OpenWeather API error: {response.StatusCode} - {jsonResponse}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var weatherData = JsonConvert.DeserializeObject<OpenWeatherRootDto>(content);

            dto.City = weatherData.name;
            dto.Temperature = weatherData.main.temp;
            dto.FeelsLike = weatherData.main.feels_like;
            dto.Humidity = weatherData.main.humidity;
            dto.Pressure = weatherData.main.pressure;
            dto.WindSpeed = weatherData.wind.speed;
            dto.WeatherCondition = weatherData.weather[0].main;

            return dto;
        }
    }
}
