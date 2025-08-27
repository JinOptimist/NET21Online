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
        private ITourPreviewRepository _tourPreviewRepository;
        private IToursRepository _toursRepository;
        private IUserRepositrory _userRepositrory;

        public TourismController(ITourPreviewRepository tourPreviewRepository,
            IToursRepository toursRepository,
            IUserRepositrory userRepositrory)
        {
            _tourPreviewRepository = tourPreviewRepository;
            _toursRepository = toursRepository;
            _userRepositrory = userRepositrory;
        }
        #region Main page
        public IActionResult Index()
        {
            var titleNames = _tourPreviewRepository
                .GetPopularListTitles()
                .Select(dbData => new TourPreviewViewModel
                {
                    Title = dbData.TourName,
                    URL = dbData.TourImgUrl,
                    Days = dbData.DaysToPrepareTour,
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
        public IActionResult AddContent(TourPreviewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var tourismBd = new TourPreview()
            {
                TourName = viewModel.Title,
                DaysToPrepareTour = (int)viewModel.Days,
                TourImgUrl = viewModel.URL,
                TourRating = 0
            };
            _tourPreviewRepository.Add(tourismBd);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            _tourPreviewRepository.Remove(id);
            return RedirectToAction("Index");
        }
        #endregion
        #region Shop
        public IActionResult Shop()
        {
            var tourItems = _toursRepository
                .GetShopItemWithAuthor()
                .Select(dbData => new TourViewModel
                {
                    TourImg = dbData.TourImgUrl,
                    TourName = dbData.TourName,
                    AuthorName = dbData.Author?.UserName?? "NoAuthor",
                    Id = dbData.Id
                }).
                ToList();

            return View(tourItems);
        }

        [HttpGet]
        public IActionResult AddShopItem()
        {
            var users = _userRepositrory.GetAll();
            var viewModel = new TourCreationViewModel();
            viewModel.AllUsers = users
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.UserName,
                }).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddShopItem(TourCreationViewModel viewModel)
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
            var author = _userRepositrory.GetFirstById(authorId);

            var tourismShopBd = new Tours()
            {
                TourName = viewModel.TourName,
                TourImgUrl = viewModel.TourImg,
                Author = author,
            };

            _toursRepository.Add(tourismShopBd);
            return RedirectToAction("Shop");
        }
        public IActionResult RemoveShopItem(int id)
        {
            _toursRepository.Remove(id);
            return RedirectToAction("Shop");
        }
        #endregion
        #region Link Author with Tour in Shop
        [HttpGet]
        public IActionResult Link()
        {
            var linkToursViewModel = new LinkTourWithAuthorViewModel();
            linkToursViewModel.AllUsers = _userRepositrory
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.UserName,
                })
                .ToList();

            linkToursViewModel.AllShopItems = _toursRepository
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
        public IActionResult Link(LinkTourWithAuthorViewModel linkTourView)
        {
            var tourShopItem = _toursRepository.GetFirstById(linkTourView.TitleNameId);
            var user = _userRepositrory.GetFirstById(linkTourView.AuthorId);

            tourShopItem.Author = user;
            _toursRepository.Update(tourShopItem);
            return RedirectToAction("Shop");
        }
        #endregion
    }
}
