using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.Tourism;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Enum;
using WebPortal.Models;
using WebPortal.Models.Tourism;
using WebPortal.Services;
using WebPortal.Services.Permissions;


namespace WebPortal.Controllers
{
    [Authorize]
    public class TourismController : Controller
    {
        private ITourPreviewRepository _tourPreviewRepository;
        private IToursRepository _toursRepository;
        private IUserRepositrory _userRepositrory;
        private AuthService _authService;
        private ITourPermission _tourPermission;
        private ITourismFilesService _tourismFilesService;

        public TourismController(ITourPreviewRepository tourPreviewRepository,
            IToursRepository toursRepository,
            IUserRepositrory userRepositrory,
            AuthService authService,
            ITourPermission tourPermission,
            ITourismFilesService tourismFilesService)
        {
            _tourPreviewRepository = tourPreviewRepository;
            _toursRepository = toursRepository;
            _userRepositrory = userRepositrory;
            _authService = authService;
            _tourPermission = tourPermission;
            _tourismFilesService = tourismFilesService;
        }
        #region Main page

        [AllowAnonymous]
        public IActionResult Index()
        {
            var viewModel = new PersonalHomeViewModel();

            if (_authService.IsAuthenticated())
            {
                var name = _authService.GetUser().UserName;
                viewModel.Name = name;
            }
            else
            {
                viewModel.Name = "Guest";
            }
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

            viewModel.TourPreviews = titleNames;

            return View(viewModel);
        }

        [HttpGet]
        [Role(Role.Admin)]
        public IActionResult AddContent()
        {

            return View();
        }


        [HttpPost]
        [Role(Role.Admin)]
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

        [AllowAnonymous]
        public IActionResult Shop()
        {
            var viewModel = new PersonalHomeViewModel();
            if (_authService.IsAuthenticated())
            {
                var id = _authService.GetId();
                var name = _authService.GetUser().UserName;

                viewModel.Id = id;
                viewModel.Name = name;
            }
            else
            {
                viewModel.Id = 0;
                viewModel.Name = "Guest";
            }

            var currentUserId = _authService.IsAuthenticated()
                ? _authService.GetId()
                : -1;
            var tourItems = _toursRepository
                .GetShopItemWithAuthor()
                .Select(dbData => new TourViewModel
                {
                    Id = dbData.Id,
                    TourImg = dbData.TourImgUrl,
                    TourName = dbData.TourName,
                    AuthorName = dbData.Author?.UserName ?? "NoAuthor",
                    CanDelete = _tourPermission.CanDelete(dbData)
                }).
                ToList();

            viewModel.Tours = tourItems;

            return View(viewModel);
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
            var ImagePath = _tourismFilesService.UploadImage(viewModel.TourImgFile);
            var tourismShopBd = new Tours()
            {
                TourName = viewModel.TourName,
                TourImgUrl = ImagePath,
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
        #region Link User with Role
        [HttpGet]
        public IActionResult LinkRole()
        {
            var linkToursViewModel = new LinkUsersWithRoleViewModel();
            linkToursViewModel.AllUsers = _userRepositrory
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.UserName,
                })
                .ToList();

            linkToursViewModel.AllRoles = System
                .Enum
                .GetValues<Role>()
                .Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = x.ToString(),
                })
                .ToList();
            return View(linkToursViewModel);
        }

        [HttpPost]
        public IActionResult LinkRole(LinkUsersWithRoleViewModel linkTourView)
        {
            var user = _userRepositrory.GetFirstById(linkTourView.AuthorId);
            user.Role = linkTourView.RoleId;
            _userRepositrory.Update(user);
            return RedirectToAction("Index","User");
        }
        #endregion
    }
}
