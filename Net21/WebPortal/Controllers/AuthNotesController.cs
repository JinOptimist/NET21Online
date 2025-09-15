using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Models.AuthNotes;

namespace WebPortal.Controllers;

public class AuthNotesController : Controller
{
    public const string AUTH_KEY = "AuthKey";
    private IUserNotesRepository _userNotesRepository;

    public AuthNotesController(IUserNotesRepository userNotesRepository)
    {
        _userNotesRepository = userNotesRepository;
    }

    [HttpGet]
    public IActionResult Login(string? ReturnUrl)
    {
        var viewModel = new AuthNotesLoginViewModel();
        viewModel.ReturnUrl = ReturnUrl;
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Login(AuthNotesLoginViewModel authNotesLoginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(authNotesLoginViewModel);
        }

        var user = _userNotesRepository.Login(
            authNotesLoginViewModel.UserName,
            authNotesLoginViewModel.Password);

        if (user == null)
        {
            ModelState
                .AddModelError(nameof(AuthNotesRegistrationViewModel.UserName), "Wrong name or password");
            return View(authNotesLoginViewModel);
        }

        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Name", user.UserName),
            new Claim("Avatar", user.AvatarUrl),
            new Claim("Role", ((int)user.Role).ToString()),
            new Claim("Language", ((int)user.Language).ToString()),
            new Claim(ClaimTypes.AuthenticationMethod, AUTH_KEY),
        };

        var identity = new ClaimsIdentity(claims, AUTH_KEY);

        var principal = new ClaimsPrincipal(identity);

        HttpContext
            .SignInAsync(principal)
            .Wait();

        return !string.IsNullOrEmpty(authNotesLoginViewModel.ReturnUrl)
            ? Redirect(authNotesLoginViewModel.ReturnUrl)
            : RedirectToAction("Index", "Notes");
    }

    [HttpGet]
    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registration(AuthNotesRegistrationViewModel authNotesRegistrationViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(authNotesRegistrationViewModel);
        }

        _userNotesRepository.Registration(
            authNotesRegistrationViewModel.UserName,
            authNotesRegistrationViewModel.Password
        );

        return RedirectToAction("Login", "AuthNotes");
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync().Wait();

        return RedirectToAction("Index", "Notes");
    }

    public IActionResult IsNameUniq(string name)
    {
        Thread.Sleep(2 * 1000);
        var isUniq = _userNotesRepository.IsUniqUserName(name);
        return Json(isUniq);
    }

    public IActionResult Forbid()
    {
        return View();
    }
}