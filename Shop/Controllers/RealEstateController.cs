using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models.RealEstate;
using Shop.Core.Domain;
using Shop.Core.ServiceInterface;
using Shop.Models.Spaceships;
using Shop.ApplicationServices.Services;
using Shop.Core.Dto;

namespace Shop.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly ShopContext _context;
        private readonly IRealEstateServices _realEstateServices;

        public RealEstateController
           (
               ShopContext context,
                IRealEstateServices realEstateServices
           )
        {
            _context = context;
            _realEstateServices = realEstateServices;
        }

        public IActionResult Index()
        {
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
        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel result = new();

            return View("CreateUpdate", result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Area = vm.Area,
                Location = vm.Location,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _realEstateServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realEstate = await _realEstateServices.DetailAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = realEstate.Id;
            vm.Area = realEstate.Area;
            vm.Location = realEstate.Location;
            vm.RoomNumber = realEstate.RoomNumber;
            vm.BuildingType = realEstate.BuildingType;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;

            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Area = vm.Area,
                Location = vm.Location,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
            };

            var result = await _realEstateServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)

        {
            var realEstate = await _realEstateServices.DetailAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var vm = new RealEstateDeleteViewModel();

            vm.Id = realEstate.Id;
            vm.Area = realEstate.Area;
            vm.Location = realEstate.Location;
            vm.RoomNumber = realEstate.RoomNumber;
            vm.BuildingType = realEstate.BuildingType;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var result = await _realEstateServices.Delete(id);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {

            var realEstate = await _realEstateServices.DetailAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var vm = new RealEstateDetailsViewModel();

            vm.Id = realEstate.Id;
            vm.Area = realEstate.Area;
            vm.Location = realEstate.Location;
            vm.RoomNumber = realEstate.RoomNumber;
            vm.BuildingType = realEstate.BuildingType;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;

            return View(vm);
        }
    }
}


