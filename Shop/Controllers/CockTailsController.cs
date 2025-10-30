using Microsoft.AspNetCore.Mvc;
using Shop.ApplicationServices.Services;
using Shop.Core.ServiceInterface;
using Shop.Models.CockTails;

namespace Shop.Controllers
{
    public class CockTailsController : Controller
    {
        private readonly ICockTailsServices _cockTailsServices;

        public CockTailsController
             (
             ICockTailsServices cockTailsServices
             )
        {
            _cockTailsServices = cockTailsServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new CockTailsViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CockTailsViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.SearchTerm))
            {
                return View(model);
            }

            var dtos = await _cockTailsServices.SearchCocktailsByNameAsync(model.SearchTerm);

            if (dtos == null || dtos.Count == 0)
            {
                // No results
                return View(model);
            }

            // Take the first result
            var dto = dtos.First();

            var ingredients = new List<string>();
            var measures = new List<string>();

            AddIngredientIfNotEmpty(dto.StrIngredient1, dto.StrMeasure1, ingredients, measures);
            AddIngredientIfNotEmpty(dto.StrIngredient2, dto.StrMeasure2, ingredients, measures);
            AddIngredientIfNotEmpty(dto.StrIngredient3, dto.StrMeasure3, ingredients, measures);
            AddIngredientIfNotEmpty(dto.StrIngredient4, dto.StrMeasure4, ingredients, measures);
            AddIngredientIfNotEmpty(dto.StrIngredient5, dto.StrMeasure5, ingredients, measures);
            AddIngredientIfNotEmpty(dto.StrIngredient6, dto.StrMeasure6, ingredients, measures);
            AddIngredientIfNotEmpty(dto.StrIngredient7, dto.StrMeasure7, ingredients, measures);
            AddIngredientIfNotEmpty(dto.StrIngredient8, dto.StrMeasure8, ingredients, measures);
            AddIngredientIfNotEmpty(dto.StrIngredient9, dto.StrMeasure9, ingredients, measures);
            AddIngredientIfNotEmpty(dto.StrIngredient10, dto.StrMeasure10, ingredients, measures);
            AddIngredientIfNotEmpty(dto.StrIngredient11, dto.StrMeasure11, ingredients, measures);
            AddIngredientIfNotEmpty(dto.StrIngredient12, dto.StrMeasure12, ingredients, measures);
            AddIngredientIfNotEmpty(dto.StrIngredient13, dto.StrMeasure13, ingredients, measures);
            AddIngredientIfNotEmpty(dto.StrIngredient14, dto.StrMeasure14, ingredients, measures);
            AddIngredientIfNotEmpty(dto.StrIngredient15, dto.StrMeasure15, ingredients, measures);

            // Map to model
            model.Id = dto.IdDrink;
            model.Name = dto.StrDrink;
            model.Category = dto.StrCategory;
            model.Glass = dto.StrGlass;
            model.Instructions = dto.StrInstructions;
            model.ImageUrl = dto.StrDrinkThumb;
            model.IsAlcoholic = dto.StrAlcoholic;
            model.Tags = dto.StrTags;
            model.Ingredients = ingredients;
            model.Measures = measures;

            return View(model);
        }

        private void AddIngredientIfNotEmpty(string ingredient, string measure, List<string> ingredients, List<string> measures)
        {
            if (!string.IsNullOrWhiteSpace(ingredient))
            {
                ingredients.Add(ingredient.Trim());
                measures.Add(string.IsNullOrWhiteSpace(measure) ? "" : measure.Trim());
            }
        }
    }
}