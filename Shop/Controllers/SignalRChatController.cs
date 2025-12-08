using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class SignalRChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
