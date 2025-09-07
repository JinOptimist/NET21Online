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
        private IWebHostEnvironment _webHostEnvironment;

        public SpaceStationController(
            ISpaceStationRepository spaceStationRepository,
            IUserRepositrory userRepositrory,
            AuthService authService,
            ISpaceNewsPermission spaceNewsPermission,
            IWebHostEnvironment webHostEnvironment)
        {
            _spaceStationRepository = spaceStationRepository;
            _userRepositrory = userRepositrory;
            _authService = authService;
            _spaceNewsPermission = spaceNewsPermission;
            _webHostEnvironment = webHostEnvironment;
        }
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
                    CanRemove = _spaceNewsPermission.CanRemove(dbSpaceNews),
                    SourceUrl = null
                })
                .ToList();

            if (_authService.IsAuthenticated())
            {
                var userId = _authService.GetId();
                var fileName = $"{userId}.pdf";
                var wwwRootPath = _webHostEnvironment.WebRootPath;
                var filePath = System.IO.Path.Combine(wwwRootPath, "documents", fileName);

                if (System.IO.File.Exists(filePath))
                {
                    foreach (var news in SpaceNews)
                    {
                        news.SourceUrl = $"/documents/{fileName}";
                    }
                }
            }

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
                var userId = _authService.GetId();
                spaceNewsViewModel.SourceUrl = $"/documents{userId}.pdf";

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

        [HttpPost]
        [Authorize]
        public IActionResult AttachSource(IFormFile source)
        {
            var userId = _authService.GetId();
            var fileName = $"{userId}.pdf";
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var path = System.IO.Path.Combine(wwwRootPath, "documents", fileName);

            using (var fileStreamOnOurServer = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var streamFromClientFileSystem = source.OpenReadStream())
                {
                    streamFromClientFileSystem.CopyToAsync(fileStreamOnOurServer).Wait();
                }
            }

            return RedirectToAction("Index");
        }
    }
}