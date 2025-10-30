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
    public class CockTailsServices : ICockTailsServices
    {
        private readonly HttpClient _httpClient;
        public CockTailsServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<CockTailsDto>> SearchCocktailsByNameAsync(string name)
        {
            var response = await _httpClient.GetStringAsync(
                          $"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={name}");


            var apiResponse = JsonConvert.DeserializeObject<dynamic>(response);

            if (apiResponse.drinks == null)
                return new List<CockTailsDto>();

            var cocktails = new List<CockTailsDto>();

            foreach (var drink in apiResponse.drinks)
            {
                cocktails.Add(new CockTailsDto
                {
                    IdDrink = drink.idDrink,
                    StrDrink = drink.strDrink,
                    StrDrinkAlternate = drink.strDrinkAlternate,
                    StrTags = drink.strTags,
                    StrVideo = drink.strVideo,
                    StrCategory = drink.strCategory,
                    StrIBA = drink.strIBA,
                    StrAlcoholic = drink.strAlcoholic,
                    StrGlass = drink.strGlass,
                    StrInstructions = drink.strInstructions,
                    StrDrinkThumb = drink.strDrinkThumb,
                    StrIngredient1 = drink.strIngredient1,
                    StrIngredient2 = drink.strIngredient2,
                    StrIngredient3 = drink.strIngredient3,
                    StrIngredient4 = drink.strIngredient4,
                    StrIngredient5 = drink.strIngredient5,
                    StrIngredient6 = drink.strIngredient6,
                    StrIngredient7 = drink.strIngredient7,
                    StrIngredient8 = drink.strIngredient8,
                    StrIngredient9 = drink.strIngredient9,
                    StrIngredient10 = drink.strIngredient10,
                    StrIngredient11 = drink.strIngredient11,
                    StrIngredient12 = drink.strIngredient12,
                    StrIngredient13 = drink.strIngredient13,
                    StrIngredient14 = drink.strIngredient14,
                    StrIngredient15 = drink.strIngredient15,
                    StrMeasure1 = drink.strMeasure1,
                    StrMeasure2 = drink.strMeasure2,
                    StrMeasure3 = drink.strMeasure3,
                    StrMeasure4 = drink.strMeasure4,
                    StrMeasure5 = drink.strMeasure5,
                    StrMeasure6 = drink.strMeasure6,
                    StrMeasure7 = drink.strMeasure7,
                    StrMeasure8 = drink.strMeasure8,
                    StrMeasure9 = drink.strMeasure9,
                    StrMeasure10 = drink.strMeasure10,
                    StrMeasure11 = drink.strMeasure11,
                    StrMeasure12 = drink.strMeasure12,
                    StrMeasure13 = drink.strMeasure13,
                    StrMeasure14 = drink.strMeasure14,
                    StrMeasure15 = drink.strMeasure15
                });
            }

            return cocktails;
        }
    }
}
