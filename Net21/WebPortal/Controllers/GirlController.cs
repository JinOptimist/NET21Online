using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Enum;
using WebPortal.Models.Girls;
using WebPortal.Services;
using WebPortal.Services.Permissions;

namespace WebPortal.Controllers
{
    [Authorize]
    [NoBadWord]
    public class GirlController : Controller
    {
        private IGirlRepository _girlRepository;
        private IUserRepositrory _userRepositrory;
        private AuthService _authService;
        private IGirlPermission _girlPermission;

        public GirlController(
            IGirlRepository girlRepository,
            IUserRepositrory userRepositrory,
            AuthService authService,
            IGirlPermission girlPermission)
        {
            _girlRepository = girlRepository;
            _userRepositrory = userRepositrory;
            _authService = authService;
            _girlPermission = girlPermission;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var girls = _girlRepository
                .GetMostPopular()
                .Select(dbGirl =>
                    new GirlViewModel
                    {
                        Id = dbGirl.Id,
                        Name = dbGirl.Name,
                        CreationTime = DateTime.Now,
                        Src = dbGirl.Url,
                        Rating = dbGirl.Size * 2 + 20 - dbGirl.Age,
                        AuthorName = dbGirl.Author?.UserName ?? "NoAuthor",
                        CanDelete = _girlPermission.CanDelete(dbGirl),
                    })
                .ToList();

            return View(girls);
        }

        public IActionResult Remove(int Id)
        {
            _girlRepository.Remove(Id);

            return RedirectToAction("Index");
        }

        // /Girl/Add  <== HTTP GET
        [HttpGet]
        [Role(Role.GrilModrator, Role.Admin)]
        public IActionResult Add()
        {
            var users = _userRepositrory.GetAll();
            var viewModel = new GirlCreationViewModel();
            viewModel.AllUsers = users
                .Select(x => new SelectListItem
                {
                    Text = x.UserName,
                    Value = x.Id.ToString()
                })
                .ToList();

            return View(viewModel);
        }

        // /Girl/Add  <== HTTP POST
        [HttpPost]
        [Role(Role.GrilModrator, Role.Admin)]
        public IActionResult Add(GirlCreationViewModel girlViewModel)
        {
            if (!ModelState.IsValid)
            {
                var users = _userRepositrory.GetAll();
                girlViewModel.AllUsers = users
                    .Select(x => new SelectListItem
                    {
                        Text = x.UserName,
                        Value = x.Id.ToString()
                    })
                    .ToList();
                return View(girlViewModel);
            }

            var authorId = girlViewModel.AuthorId;
            var author = _userRepositrory.GetFirstById(authorId);

            // Remember new Girl
            var girlDb = new Girl()
            {
                Age = 18,
                Name = girlViewModel.Name,
                Size = 5,
                Url = girlViewModel.Src,
                Author = author
            };
            _girlRepository.Add(girlDb);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Link()
        {
            var linkGirlView = new LinkGirlViewModel();
            linkGirlView.AllGirls = _girlRepository
                .GetAllWithAuthor()
                .OrderBy(x => x.Author?.Id ?? -1)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();

            linkGirlView.AllUsers = _userRepositrory
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.UserName,
                    Value = x.Id.ToString()
                })
                .ToList();

            return View(linkGirlView);
        }

        [HttpPost]
        public IActionResult Link(LinkGirlViewModel linkGirlView)
        {
            var user = _userRepositrory.GetFirstById(linkGirlView.AuthorId);
            var girl = _girlRepository.GetFirstById(linkGirlView.GirlId);

            girl.Author = user;
            _girlRepository.Update(girl);

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult UpdateName(int id, string name)
        {
            var user = _authService.GetUser();
            
            var girl = _girlRepository.GetFirstById(id);
            if (girl.Author != user)
            {
                return Json(false);
            }

            girl.Name = name;
            _girlRepository.Update(girl);

            return Json(true);
        }
    }
}
