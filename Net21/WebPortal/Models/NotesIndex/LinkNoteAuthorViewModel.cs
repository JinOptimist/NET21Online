using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPortal.Models.NotesIndex;

public class LinkNoteAuthorViewModel
{
    public int AuthorId { get; set; }
    public List<SelectListItem> AllUsers { get; set; } = new List<SelectListItem>();

    public int NoteId { get; set; }
    public List<SelectListItem> AllNotes { get; set; } = new List<SelectListItem>();
}