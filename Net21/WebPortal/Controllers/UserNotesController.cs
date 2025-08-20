using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Models.Notes;

namespace WebPortal.Controllers;

public class UserNotesController : Controller
{
    private IUserNotesRepository _userNotesRepository;

    public UserNotesController(IUserNotesRepository userNotesRepository)
    {
        _userNotesRepository = userNotesRepository;
    }

    public IActionResult Index()
    {
        var userNotesViewModels = _userNotesRepository
            .GetAll()
            .Select(x => new UserNotesViewModel
            {
                UserName = x.UserName,
            })
            .ToList();

        return View(userNotesViewModels);
    }

    [HttpGet]
    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registration(UserNotesViewModel userNotesViewModel)
    {
        var userDb = new User
        {
            UserName = userNotesViewModel.UserName,
            Password = userNotesViewModel.Password,
            AvatarUrl = userNotesViewModel.AvatarUrl,
            Money = userNotesViewModel.Money,
        };
        _userNotesRepository.Add(userDb);
        return RedirectToAction("Index");
    }
}