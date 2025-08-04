using WebPortal.Models.NotesIndex;

namespace WebPortal.Models;

public class NotesIndexViewModel
{
    public List<Category> Categories { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Note> Notes { get; set; }
    public List<Banner> Banners { get; set; }
}