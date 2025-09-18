using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class KindergartenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
