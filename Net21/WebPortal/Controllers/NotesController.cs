using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models.Notes;
using WebPortal.Models;
using WebPortal.Models.NotesIndex;

namespace WebPortal.Controllers;

public class NotesController : Controller
{
    private NotesDbContext _notesDbContext;
    
    public NotesController(NotesDbContext notesDbContext)
    {
        _notesDbContext = notesDbContext;
    }
    
    public IActionResult Index()
    {
        var viewModel = new NotesIndexViewModel
        {
            Categories = _notesDbContext.Categories
                .Select(c => new CategoryViewModel
                {
                    Name = c.Name
                })
                .ToList(),

            Tags = _notesDbContext.Tags
                .Select(t => new TagViewModel
                {
                    Name = t.Name
                })
                .ToList(),

            Notes = _notesDbContext.Notes
                .Include(n => n.Category)
                .Include(n => n.NoteTags)
                .ThenInclude(nt => nt.Tag)
                .Select(n => new NoteViewModel
                {
                    Title = n.Title,
                    Description = n.Description,
                    ImageUrl = n.ImageUrl,
                    Category = n.Category != null
                        ? new CategoryViewModel { Name = n.Category.Name }
                        : null,
                    Tags = n.NoteTags
                        .Select(nt => new TagViewModel { Name = nt.Tag.Name })
                        .ToList()
                })
                .ToList(),

            Banners = _notesDbContext.Banners
                .Select(b => new BannerViewModel
                {
                    Name = b.Name,
                    ImageUrl = b.ImageUrl,
                    Url = b.Url
                })
                .ToList()
        };

        return View(viewModel);
    }
    
    // /Notes/Add (GET)
    [HttpGet]
    public IActionResult Add()
    {
        var viewModel = new NoteFormViewModel
        {
            CategoryList = new SelectList(_notesDbContext.Categories, "Id", "Name"),

            TagList = new MultiSelectList(_notesDbContext.Tags, "Id", "Name")
        };

        return View(viewModel);
    }

    // /Notes/Add (POST)
    [HttpPost]
    public IActionResult Add(NoteFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.CategoryList = new SelectList(_notesDbContext.Categories, "Id", "Name");
            viewModel.TagList = new MultiSelectList(_notesDbContext.Tags, "Id", "Name");

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
            foreach (var tagId in viewModel.TagIds)
            {
                note.NoteTags.Add(new NoteTag { TagId = tagId });
            }
        }

        _notesDbContext.Notes.Add(note);
        _notesDbContext.SaveChanges();

        return RedirectToAction("Index");
    }
    
    // /Notes/AddCategory (GET)
    [HttpGet]
    public IActionResult AddCategory()
    {
        return View();
    }

    // /Notes/AddCategory (POST)
    [HttpPost]
    public IActionResult AddCategory(CategoryViewModel viewModel)
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

        _notesDbContext.Categories.Add(category);
        _notesDbContext.SaveChanges();

        return RedirectToAction("Index");
    }
    
    // /Notes/AddTag (GET)
    [HttpGet]
    public IActionResult AddTag()
    {
        return View();
    }
    // /Notes/AddTag (POST)
    [HttpPost]
    public IActionResult AddTag(TagViewModel viewModel)
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

        _notesDbContext.Tags.Add(tag);
        _notesDbContext.SaveChanges();

        return RedirectToAction("Index");
    }
}