using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class RealEstateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
