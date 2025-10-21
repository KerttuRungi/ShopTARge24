using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class ChuckNorrisController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
