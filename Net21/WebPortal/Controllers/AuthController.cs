using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Enum;
using WebPortal.Models.Auth;

namespace WebPortal.Controllers
{
    public class AuthController : Controller
    {
        public const string AUTH_KEY = "Smile";
        private IUserRepositrory _userRepositrory;

        public AuthController(IUserRepositrory userRepositrory)
        {
            _userRepositrory = userRepositrory;
        }

        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            var viewModel = new AuthViewModel();
            viewModel.ReturnUrl = ReturnUrl;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Login(AuthViewModel authViewModel)
        {
            var user = _userRepositrory.Login(
                authViewModel.UserName,
                authViewModel.Password);

            if (user == null)
            {
                ModelState
                    .AddModelError(nameof(AuthViewModel.UserName), "Wrong name or password");
                return View(authViewModel);
            }

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.UserName),
                new Claim("Avatar", user.AvatarUrl),
                new Claim("Role", ((int)user.Role).ToString()),
                new Claim("Language", ((int)user.Language).ToString()),
                new Claim (ClaimTypes.AuthenticationMethod, AUTH_KEY),
            };

            var identity = new ClaimsIdentity(claims, AUTH_KEY);

            var principal = new ClaimsPrincipal(identity);

            HttpContext
                .SignInAsync(principal)
                .Wait();

            return !string.IsNullOrEmpty(authViewModel.ReturnUrl)
                ? Redirect(authViewModel.ReturnUrl)
                : RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(AuthViewModel authViewModel)
        {
            var user = _userRepositrory.GetByName(authViewModel.UserName);
            if (user is not null)
            {
                return View(authViewModel);
            }

            _userRepositrory.Registration(
                authViewModel.UserName,
                authViewModel.Password);

            return Login(authViewModel);
        }

        public IActionResult IsNameUniq(string name)
        {
            Thread.Sleep(2 * 1000);
            var isUniq = _userRepositrory.GetByName(name) == null;
            return Json(isUniq);
        }

        public IActionResult Forbid()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }
    }
}
