namespace WebPortal.DbStuff.Models.Notes;

public class Category : BaseModel
{
    public string Name { get; set; }
    public ICollection<Note> Notes { get; set; } = new List<Note>();
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}