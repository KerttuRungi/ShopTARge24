using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models.Kindergarten;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.ApplicationServices.Services;
using Shop.Models.Spaceships;
using Shop.Core.Domain;


namespace Shop.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopContext _context;
        private readonly IKindergartenServices _kindergartenServices;

        public KindergartenController
            (
                ShopContext context,
                IKindergartenServices kindergartenServices
            )
        {
            _context = context;
            _kindergartenServices = kindergartenServices;
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
        
        [HttpGet]
        public IActionResult Create()
        {
            KindergartenCreateUpdateViewModel result = new();

            return View("CreateUpdate", result);
            
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(KindergartenCreateUpdateViewModel vm)
        {
            var dto = new KindergartenDto()
            {
                GroupName = vm.GroupName,
                ChidlrenCount = vm.ChidlrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
            };

            var result = await _kindergartenServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }



            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }
            var vm = new KindergartenCreateUpdateViewModel();

            vm.Id = kindergarten.Id;
            vm.GroupName = kindergarten.GroupName;
            vm.ChidlrenCount = kindergarten.ChidlrenCount;
            vm.KindergartenName = kindergarten.KindergartenName;
            vm.TeacherName = kindergarten.TeacherName;
            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(KindergartenCreateUpdateViewModel vm)
        {
            var dto = new KindergartenDto()
            {
                Id = (Guid)vm.Id,
                GroupName = vm.GroupName,
                ChidlrenCount = vm.ChidlrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
            };

            var result = await _kindergartenServices.Update(dto); // Fix: Declare 'result' variable here  

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)

        {
            var kindergarten = await _kindergartenServices.DetailAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }

            var vm = new KindergartenDeleteViewModel();

            vm.Id = kindergarten.Id;
            vm.GroupName = kindergarten.GroupName;
            vm.ChidlrenCount = kindergarten.ChidlrenCount;
            vm.TeacherName = kindergarten.TeacherName;
            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var result = await _kindergartenServices.Delete(id);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {

            var kindergarten = await _kindergartenServices.DetailAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }

            var vm = new KindergartenDetailsViewModel();

            vm.Id = kindergarten.Id;
            vm.GroupName = kindergarten.GroupName;
            vm.ChidlrenCount = kindergarten.ChidlrenCount;
            vm.TeacherName = kindergarten.TeacherName;
            vm.KindergartenName = kindergarten.KindergartenName;
            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;

            return View(vm);
        }
    }
}