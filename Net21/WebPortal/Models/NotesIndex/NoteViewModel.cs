namespace WebPortal.Models.NotesIndex;

public class NoteViewModel
{
    public string Title { get; set; }
    public string? ImageUrl { get; set; }
    public string Description { get; set; }
    public CategoryViewModel? Category { get; set; }
    public List<TagViewModel> Tags { get; set; } = new();
}