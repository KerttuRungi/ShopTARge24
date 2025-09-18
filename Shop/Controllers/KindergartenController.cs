using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models.Kindergarten;


namespace Shop.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopContext _context;

        public KindergartenController
            (
                ShopContext context
            )
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var result = _context.Kindergarten
                .Select(x => new KindergartenIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    ChidlrenCount = x.ChidlrenCount,
                    KindergartenName = x.KindergartenName,
                    TeacherName = x.TeacherName,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                });

            return View(result);
        }
    }
}