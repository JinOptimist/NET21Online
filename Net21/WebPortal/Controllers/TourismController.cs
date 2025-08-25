using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private IUserRepositrory _userRepositrory;

        public TourismController(ITourismRepository tourismRepository,
            ITourismShopRepository shopRepository,
            IUserRepositrory userRepositrory)
        {
            _tourismRepository = tourismRepository;
            _shopRepository = shopRepository;
            _userRepositrory = userRepositrory;
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
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
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
                .GetShopItemWithAuthor()
                .Select(dbData => new ShopViewModel
                {
                    TourImg = dbData.TourImg,
                    TourName = dbData.TourName,
                    Author = dbData.AuthorName?.UserName?? dbData.NewAuthor??"NoAuthor",
                    Id = dbData.Id
                }).
                ToList();

            return View(tourItems);
        }

        [HttpGet]
        public IActionResult AddShopItem()
        {
            var users = _userRepositrory.GetAll();
            var viewModel = new ShopViewModel();
            viewModel.AllUsers = users
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.UserName,
                }).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddShopItem(ShopViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var users = _userRepositrory.GetAll();
                viewModel.AllUsers = users
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.UserName,
                    }).ToList();
                return View(viewModel);
            }
            var authorId = viewModel.AuthorId;
            var author = _userRepositrory.GetFirstByIdWhereNull(authorId);

            var tourismShopBd = new TourismShop()
            {
                TourName = viewModel.TourName,
                TourImg = viewModel.TourImg,
                AuthorName = author,
                NewAuthor = viewModel.Author
            };

            _shopRepository.Add(tourismShopBd);
            return RedirectToAction("Shop");
        }
        public IActionResult RemoveShopItem(int id)
        {
            _shopRepository.Remove(id);
            return RedirectToAction("Shop");
        }
        //---------------------Link Users with Shop Item------------------------
        [HttpGet]
        public IActionResult Link()
        {
            var linkToursViewModel = new LinkTourViewModel();
            linkToursViewModel.AllUsers = _userRepositrory
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.UserName,
                })
                .ToList();

            linkToursViewModel.AllShopItems = _shopRepository
                .GetAll() 
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.TourName,
                })
                .ToList();
            return View(linkToursViewModel);
        }

        [HttpPost]
        public IActionResult Link(LinkTourViewModel linkTourView)
        {
            var tourShopItem = _shopRepository.GetFirstById(linkTourView.TitleNameId);
            var user = _userRepositrory.GetFirstById(linkTourView.AuthorId);

            tourShopItem.AuthorName = user;
            _shopRepository.Update(tourShopItem);
            return RedirectToAction("Shop");
        }
    }
}
