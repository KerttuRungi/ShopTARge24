using Microsoft.AspNetCore.Mvc;
using Shop.ApplicationServices.Services;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Models.AccuWeathers;
using Shop.Models.ChuckNorris;

namespace Shop.Controllers
{
    public class chuckNorrisController : Controller
    {
        private readonly IChuckNorrisServices _chuckNorrisServices;

        public chuckNorrisController
            (
            IChuckNorrisServices chuckNorrisServices
            )
        {
            _chuckNorrisServices = chuckNorrisServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Call your service to get a random joke
            var dto = await _chuckNorrisServices.GetRandomJokeAsync();

            // Map DTO to ViewModel
            var vm = new ChuckNorrisViewModel
            {
                JokeText = dto.Value,
                IconUrl = dto.IconUrl,
                Url = dto.Url
            };

            // Pass ViewModel to view
            return View(vm);
        }
    }
}
