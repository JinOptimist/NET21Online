namespace WebPortal.Models.Notes;

public class NoteViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? ImageUrl { get; set; }
    public string Description { get; set; }
    public CategoryViewModel? Category { get; set; }
    public List<TagViewModel> Tags { get; set; } = new();
    public string Author { get; set; }
    public bool IsAllowedToDelete  { get; set; }
}