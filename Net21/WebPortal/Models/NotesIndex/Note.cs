namespace WebPortal.Models.NotesIndex;

public class Note
{
    public string Title { get; set; }
    public string? ImageUrl { get; set; }
    public string Description { get; set; }
    public Category? Category { get; set; }
    public List<Tag> Tags { get; set; }
}