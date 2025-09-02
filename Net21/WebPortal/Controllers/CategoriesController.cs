using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Enum;
using WebPortal.Models.Notes;

namespace WebPortal.Controllers;

[Authorize]
public class CategoriesController : Controller
{
    private ICategoryRepository _categoryRepository;

    public CategoriesController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public IActionResult Index()
    {
        var categoryViewModels = _categoryRepository
            .GetAll()
            .Select(x => new CategoryViewModel
            {
                Name = x.Name,
            })
            .ToList();

        return View(categoryViewModels);
    }
    
    // /Categories/Add (GET)
    [HttpGet]
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator)]
    public IActionResult Add()
    {
        return View();
    }

    // /Categories/Add (POST)
    [HttpPost]
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator)]
    public IActionResult Add(CategoryViewModel viewModel)
    {
        
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var category = new Category
        {
            Name = viewModel.Name,
            CreateDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow
        };

        _categoryRepository.Add(category);

        return RedirectToAction("Index", "Notes");
    }
}