namespace WebPortal.DbStuff.Models.Notes;

public class Note : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public ICollection<Note> NoteTags { get; set; } = new List<NoteTag>();
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}