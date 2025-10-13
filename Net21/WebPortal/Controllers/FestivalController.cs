using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Enum;
using WebPortal.Models.Festival;

namespace WebPortal.Controllers
{
    [Authorize]
    [NoBadWord]
    public class FestivalController : Controller
    {
        private readonly IFestivalRepository _festivalRepository;
        private readonly IGirlRepository _girlRepository;

        public FestivalController(
            IFestivalRepository festivalRepository,
            IGirlRepository girlRepository
        )
        {
            _festivalRepository = festivalRepository;
            _girlRepository = girlRepository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var items = _festivalRepository
                .GetAllWithGirls()
                .Select(x => new FestivalViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Theme = x.Theme,
                    LogoUrl = x.LogoUrl,
                    GirlsCount = x.Girls.Count,
                })
                .OrderBy(x => x.Date)
                .ToList();
            return View(items);
        }

        [HttpGet]
        [Role(Role.Admin)]
        public IActionResult Add()
        {
            var vm = new FestivalCreationViewModel
            {
                Date = DateTime.UtcNow.Date,
                Theme = FestivalTheme.Unknown,
            };
            return View(vm);
        }

        [HttpPost]
        [Role(Role.Admin)]
        public IActionResult Add(FestivalCreationViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var entity = new DbStuff.Models.Festival
            {
                Name = vm.Name,
                Slogan = vm.Slogan ?? string.Empty,
                Date = vm.Date,
                Theme = vm.Theme,
                LogoUrl = vm.LogoUrl,
                Description = vm.Description,
            };
            _festivalRepository.Add(entity);
            return RedirectToAction("Index");
        }

        [Role(Role.Admin)]
        public IActionResult Remove(int id)
        {
            _festivalRepository.Remove(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Role(Role.Admin)]
        public IActionResult LinkGirls(int id)
        {
            var festival = _festivalRepository.GetFirstById(id);
            if (festival == null)
            {
                return RedirectToAction("Index");
            }

            var vm = new FestivalLinkGirlsViewModel
            {
                FestivalId = festival.Id,
                FestivalName = festival.Name,
                SelectedGirlIds = festival.Girls.Select(g => g.Id).ToList(),
                AllGirls = _girlRepository
                    .GetAll()
                    .Select(g => new SelectListItem { Text = g.Name, Value = g.Id.ToString() })
                    .ToList(),
            };
            return View(vm);
        }

        [HttpPost]
        [Role(Role.Admin)]
        public IActionResult LinkGirls(FestivalLinkGirlsViewModel vm)
        {
            var festival = _festivalRepository
                .GetAllWithGirls()
                .FirstOrDefault(x => x.Id == vm.FestivalId);
            if (festival == null)
            {
                return RedirectToAction("Index");
            }

            var allGirls = _girlRepository.GetAll();
            var selectedGirls = allGirls.Where(g => vm.SelectedGirlIds.Contains(g.Id)).ToList();

            festival.Girls.Clear();
            foreach (var g in selectedGirls)
            {
                festival.Girls.Add(g);
            }
            _festivalRepository.Update(festival);

            return RedirectToAction("Index");
        }
    }
}
