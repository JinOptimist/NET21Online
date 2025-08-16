namespace WebPortal.DbStuff.Models.Notes;

public class NoteTag : BaseModel
{
    public int NoteId { get; set; }
    public Note Note { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}