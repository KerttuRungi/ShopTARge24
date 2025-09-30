using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models.RealEstate;
using Shop.Core.Domain;

namespace Shop.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly ShopContext _context;

        public RealEstateController(ShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
            var result = _context.RealEstate
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Area = x.Area,
                    Location = x.Location,
                    RoomNumber = x.RoomNumber,
                    BuildingType = x.BuildingType,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt
                });

            return View(result);
        }
    }
}

