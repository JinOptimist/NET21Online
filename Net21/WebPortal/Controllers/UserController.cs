using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Enum;
using WebPortal.Models.Users;
using WebPortal.Services;

namespace WebPortal.Controllers
{
    public class UserController : Controller
    {
        private IUserRepositrory _userRepositrory;
        private IFileService _fileService;
        private readonly AuthService _authService;

        public UserController(
            IUserRepositrory userRepositrory,
            AuthService authService,
            IFileService fileService)
        {
            _userRepositrory = userRepositrory;
            _authService = authService;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            var usersViewModels = _userRepositrory
                .GetAll()
                .Select(x => new UserViewModel
                {
                    UserName = x.UserName,
                    Role = x.Role
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
        public IActionResult Profile()
        {
            var viewModel = new ProfileViewModel();

            viewModel.Name = _authService.GetName();
            viewModel.Languages = System
                .Enum
                .GetValues<Language>()
                .ToList();
            viewModel.Language = _authService.GetLanguage();
            var userId = _authService.GetId();
            viewModel.AvatarUrl = $"/images/avatars/{userId}.jpg";
            viewModel.Role = _authService.GetRole();

            return View(viewModel);
        }

        [Authorize]
        public IActionResult ChangeLanguage(Language lang)
        {
            var user = _authService.GetUser();
            user.Language = lang;
            _userRepositrory.Update(user);
            return RedirectToAction("Index", "Home");
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

        [HttpPost]
        [Authorize]
        public IActionResult UpdateAvatar(IFormFile avatar)
        {
            _fileService.UploadAvatar(avatar);
            return RedirectToAction("Profile");
        }

        [Role(Role.Admin)]
        public IActionResult AllAvatars()
        {
            var users = _userRepositrory
                .GetAll()
                .Select(x => new UserIdAndNameViewModel
                {
                    Id = x.Id,
                    Name = x.UserName,
                })
                .ToList();
            return View(users);
        }

        [Role(Role.Admin)]
        public IActionResult DeleteAvatar(int userId)
        {
            _fileService.ReplaceAvatarToDefault(userId);
            return RedirectToAction("AllAvatars");
        }
    }
}
