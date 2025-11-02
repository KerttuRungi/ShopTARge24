using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Models.AccuWeathers;
using Shop.Models.OpenWeather;

namespace Shop.Controllers
{
    public class OpenWeathersController : Controller
    {
        private readonly IOpenWeatherServices _openWeatherServices;

        public OpenWeathersController
            (
            IOpenWeatherServices openWeatherServices
            )
        {
            _openWeatherServices = openWeatherServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchCity(OpenWeatherSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("CityData", "OpenWeathers", new { city = model.CityName });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CityData(string city)
        {
            OpenWeatherResultDto dto = new();
            dto.CityName = city;

            _openWeatherServices.OpenWeatherResultDto(dto);
            OpenWeatherViewModel vm = new();
            vm.City = dto.CityName;
            vm.Temperature = dto.Temperature;
            vm.Humidity = dto.Humidity;
            vm.Pressure = dto.Pressure;
            vm.WindSpeed = dto.WindSpeed;
            vm.WeatherCondition = dto.WeatherCondition;

            return View(vm);
        }
    }
}