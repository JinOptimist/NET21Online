using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortal.Models.SpaceStation;
using WebPortal.Services;
using Microsoft.AspNetCore.Authorization;
using WebPortal.Services.Permissions;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.Enum;

namespace WebPortal.Controllers
{
    public class SpaceStationController : Controller
    {
        private AuthService _authService;
        private ISpaceStationRepository _spaceStationRepository;
        private IUserRepositrory _userRepositrory;
        private ISpaceNewsPermission _spaceNewsPermission;


        public SpaceStationController(
            ISpaceStationRepository spaceStationRepository,
            IUserRepositrory userRepositrory,
            AuthService authService,
            ISpaceNewsPermission spaceNewsPermission)
        {
            _spaceStationRepository = spaceStationRepository;
            _userRepositrory = userRepositrory;
            _authService = authService;
            _spaceNewsPermission = spaceNewsPermission;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            string userName = "Guest";
            if (_authService.IsAuthenticated())
            {
                userName = _authService.GetUser().UserName;
            }

            var lastVisitCookie = Request.Cookies["SpaceStationLastVisit"];
            DateTime? lastVisit = null;

            if (lastVisitCookie != null && DateTime.TryParse(lastVisitCookie, out var parsedDate))
            {
                lastVisit = parsedDate;
            }

            Response.Cookies.Append("SpaceStationLastVisit", DateTime.Now.ToString(), new CookieOptions
            {
                Expires = DateTime.Now.AddYears(1),
                Path = "/"
            });

            ViewBag.UserName = userName;
            ViewBag.LastVisit = lastVisit;
            ViewBag.CurrentDate = DateTime.Now;

            var SpaceNews = _spaceStationRepository
                .FirstNews()
                .Select(dbSpaceNews =>
                new SpaceNewsViewModel
                {
                    Id = dbSpaceNews.Id,
                    Title = dbSpaceNews.Title,
                    DateAdded = dbSpaceNews.DateAdded,
                    ImageUrl = dbSpaceNews.Url,
                    Content = dbSpaceNews.Content,
                    AuthorName = dbSpaceNews.Author?.UserName ?? "John Doe",
                    CanRemove = _spaceNewsPermission?.CanRemove(dbSpaceNews) ?? false,
                })
                .ToList();

            return View(SpaceNews);
        }

        [HttpPost]
        [Authorize]
        [Role(Role.SpaceNewsModerator)]
        public IActionResult remove(int Id)
        {
            _spaceStationRepository.Remove(Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult News()
        {
            var users = _userRepositrory.GetAll();
            var viewModel = new SpaceNewsAddingViewModel();
            viewModel.AllUsers = users
                .Select(x => new SelectListItem
                {
                    Text = x.UserName,
                    Value = x.Id.ToString()
                })
                .ToList();

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult News(SpaceNewsAddingViewModel spaceNewsViewModel)
        {
            if (!ModelState.IsValid)
            {
                var users = _userRepositrory.GetAll();
                spaceNewsViewModel.AllUsers = users
                    .Select(x => new SelectListItem
                    {
                        Text = x.UserName,
                        Value = x.Id.ToString()
                    })
                    .ToList();
                return View(spaceNewsViewModel);
            }
            var authorId = spaceNewsViewModel.AuthorId;
            var author = _userRepositrory.GetFirstById(authorId);

            var SpaceNewsDb = new SpaceNews()
            {
                Title = spaceNewsViewModel.Title,
                DateAdded = spaceNewsViewModel.DateAdded,
                Url = spaceNewsViewModel.ImageUrl,
                Content = spaceNewsViewModel.Content,
                Author = author
            };
            _spaceStationRepository.Add(SpaceNewsDb);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Link()
        {
            var linkSpaceNews = new LinkSpaceNewsViewModel();
            linkSpaceNews.AllSpaceNews = _spaceStationRepository
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                })
                .ToList();

            linkSpaceNews.AllUsers = _userRepositrory
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.UserName,
                    Value = x.Id.ToString()
                })
                .ToList();
            return View(linkSpaceNews);
        }

        [HttpPost]
        public IActionResult Link(LinkSpaceNewsViewModel linkSpaceNewsView)
        {
            var user = _userRepositrory.GetFirstById(linkSpaceNewsView.AuthorId);
            var SpaceNews = _spaceStationRepository.GetFirstById(linkSpaceNewsView.SpaceNewsId);

            SpaceNews.Author = user;
            _spaceStationRepository.Update(SpaceNews);

            return RedirectToAction("Index");
        }
    }
}