using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Models;
using WebPortal.Models.NotesIndex;

namespace WebPortal.Controllers;

public class NotesController : Controller
{
    private INoteRepository _noteRepository;
    private ICategoryRepository _categoryRepository;
    private ITagRepository _tagRepository;
    
    public NotesController(INoteRepository noteRepository, ICategoryRepository categoryRepository, 
        ITagRepository tagRepository)
    {
        _noteRepository = noteRepository;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
    }
    
    public IActionResult Index()
    {
        var viewModel = new NotesIndexViewModel
        {
            Categories = _categoryRepository
                .GetAll()
                .Select(c => new CategoryViewModel
                {
                    Name = c.Name
                })
                .ToList(),

            Tags = _tagRepository
                .GetAll()
                .Select(t => new TagViewModel
                {
                    Name = t.Name
                })
                .ToList(),

            Notes = _noteRepository
                .GetNotesLastWeek()
                .Select(n => new NoteViewModel
                {
                    Title = n.Title,
                    Description = n.Description,
                    ImageUrl = n.ImageUrl,
                    Category = n.Category != null
                        ? new CategoryViewModel { Name = n.Category.Name }
                        : null,
                    Tags = n.Tags
                        .Select(nt => new TagViewModel { Name = nt.Name })
                        .ToList()
                })
                .ToList(),

            // Banners = _notesDbContext.Banners
            //     .Select(b => new BannerViewModel
            //     {
            //         Name = b.Name,
            //         ImageUrl = b.ImageUrl,
            //         Url = b.Url
            //     })
            //     .ToList()
        };

        return View(viewModel);
    }
    
    // /Notes/Add (GET)
    [HttpGet]
    public IActionResult Add()
    {
        var viewModel = new NoteFormViewModel
        {
            CategoryList = new SelectList(_categoryRepository.GetAll(), "Id", "Name"),

            TagList = new MultiSelectList(_tagRepository.GetAll(), "Id", "Name")
        };

        return View(viewModel);
    }

    // /Notes/Add (POST)
    [HttpPost]
    public IActionResult Add(NoteFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.CategoryList = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            viewModel.TagList = new MultiSelectList(_tagRepository.GetAll(), "Id", "Name");

            return View(viewModel);
        }

        var note = new Note
        {
            Title = viewModel.Title.Trim(),
            Description = viewModel.Description,
            ImageUrl = viewModel.ImageUrl,
            CategoryId = viewModel.CategoryId,
            CreateDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow
        };

        if (viewModel.TagIds.Count > 0)
        {
            var tags = _tagRepository.GetAll()
                .Where(t => viewModel.TagIds.Contains(t.Id))
                .ToList();

            foreach (var tag in tags)
            {
                note.Tags.Add(tag);
            }
        }

        _noteRepository.Add(note);

        return RedirectToAction("Index");
    }
}