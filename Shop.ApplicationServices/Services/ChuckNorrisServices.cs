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
    public class ChuckNorrisServices : IChuckNorrisServices
    {
        private readonly HttpClient _httpClient;

        public ChuckNorrisServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChuckNorrisJokesDto> GetRandomJokeAsync()
        {
            var response = await _httpClient.GetStringAsync("https://api.chucknorris.io/jokes/random");

           
            var apiResponse = JsonConvert.DeserializeObject<dynamic>(response);

            return new ChuckNorrisJokesDto
            {
                IconUrl = apiResponse.icon_url,
                Id = apiResponse.id,
                Url = apiResponse.url,
                Value = apiResponse.value
            };
        }
    }
}
