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
        var viewModel = new AuthNotesViewModel();
        viewModel.ReturnUrl = ReturnUrl;
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Login(AuthNotesViewModel authNotesViewModel)
    {
        var user = _userNotesRepository.Login(
            authNotesViewModel.UserName,
            authNotesViewModel.Password);

        if (user == null)
        {
            ModelState
                .AddModelError(nameof(AuthNotesViewModel.UserName), "Wrong name or password");
            return View(authNotesViewModel);
        }
        
        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Name", user.UserName),
            new Claim("Avatar", user.AvatarUrl),
            new Claim("Role", ((int)user.Role).ToString()),
            new Claim(ClaimTypes.AuthenticationMethod, AUTH_KEY),
        };
        
        var identity = new ClaimsIdentity(claims, AUTH_KEY);

        var principal = new ClaimsPrincipal(identity);

        HttpContext
            .SignInAsync(principal)
            .Wait();

        return !string.IsNullOrEmpty(authNotesViewModel.ReturnUrl)
            ? Redirect(authNotesViewModel.ReturnUrl)
            : RedirectToAction("Index", "Notes");
    }

    [HttpGet]
    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registration(AuthNotesViewModel authNotesViewModel)
    {
        _userNotesRepository.Registration(
            authNotesViewModel.UserName,
            authNotesViewModel.Password
            );

        return Login(authNotesViewModel);
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync().Wait();

        return RedirectToAction("Index", "Notes");
    }
    
    public IActionResult Forbid()
    {
        return View();
    }
}