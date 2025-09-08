using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Shop.Data;
using Shop.Models.Spaceships;

namespace Shop.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopContext _context;

        public SpaceshipsController
            (
                ShopContext context
            )
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var result = _context.Spaceships
                .Select(x => new SpaceshipIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Classification = x.Classification,
                    BuildDate = x.BuildDate,
                    Crew = x.Crew,
                });
                

            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            SpaceshipCreateViewModel result = new();

            return View("Create", result);
        }
    }
}
