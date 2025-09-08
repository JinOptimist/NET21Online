using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Enum;
using WebPortal.Models.Notes;
using WebPortal.Services;

namespace WebPortal.Controllers;

public class UserNotesController : Controller
{
    private IUserNotesRepository _userNotesRepository;
    private AuthNotesService _authNotesService;

    public UserNotesController(
        IUserNotesRepository userNotesRepository,
        AuthNotesService authNotesService)
    {
        _userNotesRepository = userNotesRepository;
        _authNotesService = authNotesService;
    }

    [Authorize]
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator)]
    public IActionResult Index()
    {
        var usersViewModels = _userNotesRepository
            .GetAll()
            .Select(x => new UserNotesViewModel
            {
                UserName = x.UserName,
            })
            .ToList();

        return View(usersViewModels);
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult Profile()
    {
        var viewModel = new ProfileNotesViewModel();

        viewModel.Name = _authNotesService.GetName();
        viewModel.Languages = System
            .Enum
            .GetValues<Language>()
            .ToList();
        viewModel.Language = _authNotesService.GetLanguage();
        var userId = _authNotesService.GetId();
        viewModel.AvatarUrl = $"/images/avatars/{userId}.jpg";

        return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    public IActionResult ChangeLanguage(Language lang)
    {
        var user = _authNotesService.GetUser();
        user.Language = lang;
        _userNotesRepository.Update(user);

        HttpContext.SignOutAsync();
        _authNotesService.SignInUser(user);
        
        return RedirectToAction("Profile", "UserNotes");
    }
}