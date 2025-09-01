using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.Users;
using WebPortal.Services;

namespace WebPortal.Controllers
{
    public class UserController : Controller
    {
        private IUserRepositrory _userRepositrory;
        private readonly AuthService _authService;

        public UserController(IUserRepositrory userRepositrory, AuthService authService)
        {
            _userRepositrory = userRepositrory;
            _authService = authService;
        }

        public IActionResult Index()
        {
            var usersViewModels = _userRepositrory
                .GetAll()
                .Select(x => new UserViewModel
                {
                    UserName = x.UserName,
                })
                .ToList();

            return View(usersViewModels);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(UserViewModel userViewModel)
        {
            var userDb = new User
            {
                UserName = userViewModel.UserName,
                Password = userViewModel.Password,
                AvatarUrl = userViewModel.AvatarUrl,
                Money = userViewModel.Money,
            };
            _userRepositrory.Add(userDb);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult CompShopProfil()
        {
            var userDb = _authService.GetUser();

            var userViewModel = new UserViewModel
            {
                UserName = userDb.UserName,
            };

            return View(userViewModel);
        }
    }
}
