using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebPortal.DbStuff.Repositories.Interfaces;
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
        public IActionResult Login()
        {
            return View();
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
                new Claim (ClaimTypes.AuthenticationMethod, AUTH_KEY),
            };

            var identity = new ClaimsIdentity(claims, AUTH_KEY);

            var principal = new ClaimsPrincipal(identity);

            HttpContext
                .SignInAsync(principal)
                .Wait();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(AuthViewModel authViewModel)
        {
            _userRepositrory.Registration(
                authViewModel.UserName,
                authViewModel.Password);

            return Login(authViewModel);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }
    }
}
