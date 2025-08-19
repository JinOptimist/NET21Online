using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Models.NotesIndex;

namespace WebPortal.Controllers;

public class TagsController : Controller
{
    private ITagRepository _tagRepository;

    public TagsController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    
    // /Tags/Add (GET)
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    // /Tags/Add (POST)
    [HttpPost]
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