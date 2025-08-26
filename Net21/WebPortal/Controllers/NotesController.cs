using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Models;
using WebPortal.Models.Notes;
using WebPortal.Services;

namespace WebPortal.Controllers;

public class NotesController : Controller
{
    private INoteRepository _noteRepository;
    private ICategoryRepository _categoryRepository;
    private ITagRepository _tagRepository;
    private IUserNotesRepository _userNotesRepository;
    private AuthNotesService _authNotesService;
    
    public NotesController(INoteRepository noteRepository, ICategoryRepository categoryRepository, 
        ITagRepository tagRepository, IUserNotesRepository userNotesRepository, AuthNotesService authNotesService)
    {
        _noteRepository = noteRepository;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
        _userNotesRepository = userNotesRepository;
        _authNotesService = authNotesService;
    }
    
    public IActionResult Index()
    {
        var viewModel = new NotesViewModel
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
                        .ToList(),
                    Author = n.Author?.UserName ?? "No Author"
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
        if (_authNotesService.IsAuthenticated())
        {
            var id = _authNotesService.GetId();
            var userName = _authNotesService.GetUser().UserName;

            viewModel.UserNotes.Id = id;
            viewModel.UserNotes.UserName = userName;
        }
        else
        {
            viewModel.UserNotes.Id = 0;
            viewModel.UserNotes.UserName = "Guest";
        }

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
    
    // /Notes/Link (GET)
    [HttpGet]
    public IActionResult Link()
    {
        var linkNoteAuthorView = new LinkNoteAuthorViewModel();
        linkNoteAuthorView.AllNotes = _noteRepository
            .GetAllWithAuthor()
            .OrderBy(x => x.Author?.Id ?? -1)
            .Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()
            })
            .ToList();

        linkNoteAuthorView.AllUsers = _userNotesRepository
            .GetAll()
            .Select(x => new SelectListItem
            {
                Text = x.UserName,
                Value = x.Id.ToString()
            })
            .ToList();

        return View(linkNoteAuthorView);
    }

    // /Notes/Link (POST)
    [HttpPost]
    public IActionResult Link(LinkNoteAuthorViewModel linkNoteAuthorViewModelView)
    {
        var user = _userNotesRepository.GetFirstById(linkNoteAuthorViewModelView.AuthorId);
        var note = _noteRepository.GetFirstById(linkNoteAuthorViewModelView.NoteId);

        note.Author = user;
        _noteRepository.Update(note);

        return RedirectToAction("Index");
    }
}