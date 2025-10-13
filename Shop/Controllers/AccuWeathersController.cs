using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class AccuWeathersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
