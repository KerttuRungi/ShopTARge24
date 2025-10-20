using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Models.AccuWeathers;

namespace Shop.Controllers
{
    public class AccuWeathersController : Controller
    {
        private readonly IWeatherForecastServices _weatherForecastServices;

        public AccuWeathersController
            (
            IWeatherForecastServices weatherForecastServices
            )
        {
            _weatherForecastServices = weatherForecastServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchCity(AccuWeathersSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "AccuWeathers", new { city = model.CityName });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult City(string city)
        {
            AccuLocationWeatherResultDto dto = new();
            dto.CityName = city;

            _weatherForecastServices.AccuWeatherResult(dto);
            AccuWeatherViewModel vm = new();
            //vm.CityName = dto.CityName;
            vm.TempMinCelsius = dto.TemperatureMetricValue;

            return View(vm);
        }
    }
}
