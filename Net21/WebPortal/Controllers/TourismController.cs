using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.Tourism;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models;
using WebPortal.Models.Tourism;

namespace WebPortal.Controllers
{
    public class TourismController : Controller
    {
        private ITourismRepository _tourismRepository;
        private ITourismShopRepository _shopRepository;

        public TourismController(ITourismRepository tourismRepository,
            ITourismShopRepository shopRepository)
        {
            _tourismRepository = tourismRepository;
            _shopRepository = shopRepository;
        }
        //---------------Main Page---------------------
        public IActionResult Index()
        {
            var titleNames = _tourismRepository
                .GetPopularListTitles()
                .Select(dbData => new TourismViewModel
                {
                    Title = dbData.Title,
                    URL = dbData.Url,
                    Days = dbData.Days,
                    Id = dbData.Id
                }).
                ToList();

            return View(titleNames);
        }

        [HttpGet]
        public IActionResult AddContent()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddContent(TourismViewModel viewModel)
        {
            var tourismBd = new Tourism()
            {
                Title = viewModel.Title,
                Days = (int)viewModel.Days,
                Url = viewModel.URL,
                TitleRating = 0
            };
            _tourismRepository.Add(tourismBd);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            _tourismRepository.Remove(id);
            return RedirectToAction("Index");
        }

        //-----------------Shop--------------------
        public IActionResult Shop()
        {
            var tourItems = _shopRepository
                .GetAll()
                .Select(dbData => new ShopViewModel
                {
                    TourImg = dbData.TourImg,
                    TourName = dbData.TourName,
                    Author = dbData.AuthorName,
                    Id = dbData.Id
                }).
                ToList();

            return View(tourItems);
        }

        [HttpGet]
        public IActionResult AddShopItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddShopItem(ShopViewModel viewModel)
        {
            var tourismShopBd = new TourismShop()
            {
                TourName = viewModel.TourName,
                TourImg = viewModel.TourImg,
                AuthorName = viewModel.Author,
            };

            _shopRepository.Add(tourismShopBd);
            return RedirectToAction("Shop");
        }
        public IActionResult RemoveShopItem(int id)
        {
            _shopRepository.Remove(id);
            return RedirectToAction("Shop");
        }
    }
}
