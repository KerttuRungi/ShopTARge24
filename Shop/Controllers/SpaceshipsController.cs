using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class SpaceshipsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
