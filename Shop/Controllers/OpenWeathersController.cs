using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Models.OpenWeather;

namespace Shop.Controllers
{
    public class OpenWeathersController : Controller
    {
        private readonly IOpenWeatherServices _openWeatherServices;

        public OpenWeathersController(IOpenWeatherServices openWeatherServices)
        {
            _openWeatherServices = openWeatherServices;
        }

        public IActionResult Index()
        {
            return View(new OpenWeatherSearchViewModel());
        }

        [HttpPost]
        public IActionResult SearchCity(OpenWeatherSearchViewModel model)
        {
            if (ModelState.IsValid)
                return RedirectToAction("CityData", new { city = model.Name });

            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> CityData(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return RedirectToAction("Index");

            var dto = new OpenWeatherResultDto { City = city };
            dto = await _openWeatherServices.OpenWeatherResultDto(dto);

            var vm = new OpenWeatherViewModel
            {
                City = dto.City,
                Temperature = dto.Temperature,
                FeelsLike = dto.FeelsLike,
                Humidity = dto.Humidity,
                Pressure = dto.Pressure,
                WindSpeed = dto.WindSpeed,
                WeatherCondition = dto.WeatherCondition
            };

            return View("CityData", vm);
        }
    }
}
