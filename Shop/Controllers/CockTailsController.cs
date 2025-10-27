using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class CockTailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
