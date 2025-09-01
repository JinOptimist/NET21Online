using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Enum;
using WebPortal.Models.Notes;

namespace WebPortal.Controllers;

[Authorize]
public class TagsController : Controller
{
    private ITagRepository _tagRepository;

    public TagsController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public IActionResult Index()
    {
        var tagViewModels = _tagRepository
            .GetAll()
            .Select(x => new TagViewModel
            {
                Name = x.Name,
            })
            .ToList();

        return View(tagViewModels);
    }

    // /Tags/Add (GET)
    [HttpGet]
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator)]
    public IActionResult Add()
    {
        return View();
    }

    // /Tags/Add (POST)
    [HttpPost]
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator)]
    public IActionResult Add(TagViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var tag = new Tag
        {
            Name = viewModel.Name,
            CreateDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow
        };

        _tagRepository.Add(tag);

        return RedirectToAction("Index", "Notes");
    }
}