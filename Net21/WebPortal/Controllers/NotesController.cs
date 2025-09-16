using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Enum;
using WebPortal.Models.Notes;
using WebPortal.Services;
using WebPortal.Services.Permissions;

namespace WebPortal.Controllers;

[Authorize]
public class NotesController : Controller
{
    private INoteRepository _noteRepository;
    private ICategoryRepository _categoryRepository;
    private ITagRepository _tagRepository;
    private IUserNotesRepository _userNotesRepository;
    private AuthNotesService _authNotesService;
    private INotePermission _notePermission;

    public NotesController(
        INoteRepository noteRepository,
        ICategoryRepository categoryRepository,
        ITagRepository tagRepository,
        IUserNotesRepository userNotesRepository,
        AuthNotesService authNotesService,
        INotePermission notePermission)
    {
        _noteRepository = noteRepository;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
        _userNotesRepository = userNotesRepository;
        _authNotesService = authNotesService;
        _notePermission = notePermission;
    }

    [AllowAnonymous]
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
                    Id = n.Id,
                    Title = n.Title,
                    Description = n.Description,
                    ImageUrl = n.ImageUrl,
                    Category = n.Category != null
                        ? new CategoryViewModel { Name = n.Category.Name }
                        : null,
                    Tags = n.Tags
                        .Select(nt => new TagViewModel { Name = nt.Name })
                        .ToList(),
                    Author = n.Author?.UserName ?? "No Author",
                    IsAllowedToDelete = _notePermission.IsAllowedToDelete(n)
                })
                .ToList(),
            UserNotes = new UserNotesViewModel
            {
                Id = 0,
                UserName = "Guest"
            }

            // Banners = _notesDbContext.Banners
            //     .Select(b => new BannerViewModel
            //     {
            //         Name = b.Name,
            //         ImageUrl = b.ImageUrl,
            //         Url = b.Url
            //     })
            //     .ToList()
        };

        if (!_authNotesService.IsAuthenticated())
        {
            return View(viewModel);
        }

        viewModel.UserNotes.Id = _authNotesService.GetId();
        viewModel.UserNotes.UserName = _authNotesService.GetUser().UserName;

        return View(viewModel);
    }

    // /Notes/Add (GET)
    [HttpGet]
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator)]
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
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator)]
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
            UpdateDate = DateTime.UtcNow,
            Author = _authNotesService.GetUser()
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
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator)]
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
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator)]
    public IActionResult Link(LinkNoteAuthorViewModel linkNoteAuthorViewModelView)
    {
        var user = _userNotesRepository.GetFirstById(linkNoteAuthorViewModelView.AuthorId);
        var note = _noteRepository.GetFirstById(linkNoteAuthorViewModelView.NoteId);

        note.Author = user;
        _noteRepository.Update(note);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var note = _noteRepository.GetNoteWithTags(id);

        if (!_notePermission.IsAllowedToEdit(note))
        {
            return Forbid();
        }

        var selectedTagIds = note.Tags.Select(t => t.Id).ToList();

        var viewModel = new NoteFormViewModel
        {
            Title = note.Title,
            Description = note.Description,
            ImageUrl = note.ImageUrl,
            CategoryId = note.CategoryId,
            TagIds = selectedTagIds,
            CategoryList = new SelectList(
                _categoryRepository.GetAll(),
                "Id",
                "Name",
                note.CategoryId
            ),
            TagList = new MultiSelectList(
                _tagRepository.GetAll(),
                "Id",
                "Name",
                selectedTagIds
            )
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Edit(int id, NoteFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.CategoryList = new SelectList(
                _categoryRepository.GetAll(),
                "Id",
                "Name",
                viewModel.CategoryId
            );
            viewModel.TagList = new MultiSelectList(
                _tagRepository.GetAll(),
                "Id",
                "Name",
                viewModel.TagIds
            );

            return View(viewModel);
        }

        var note = _noteRepository.GetNoteWithTags(id);

        if (!_notePermission.IsAllowedToEdit(note))
        {
            return Forbid();
        }

        note.Title = viewModel.Title.Trim();
        note.Description = viewModel.Description;
        note.ImageUrl = viewModel.ImageUrl;
        note.CategoryId = viewModel.CategoryId;
        note.UpdateDate = DateTime.UtcNow;

        note.Tags.Clear();
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

        _noteRepository.Update(note);

        return RedirectToAction("Index");
    }

    public IActionResult UpdateTitle(int id, string title)
    {
        var user = _authNotesService.GetUser();

        var note = _noteRepository.GetFirstById(id);
        if (note.Author != user)
        {
            return Json(false);
        }

        note.Title = title;
        _noteRepository.Update(note);

        return Json(true);
    }

    public IActionResult Remove(int Id)
    {
        _noteRepository.Remove(Id);

        return RedirectToAction("Index");
    }
}