using Microsoft.AspNetCore.Mvc;

namespace Shop.Models.ChuckNorris
{
    public class ChuckNorrisViewModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
