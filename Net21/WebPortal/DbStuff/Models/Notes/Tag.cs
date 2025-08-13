namespace WebPortal.DbStuff.Models.Notes;

public class Tag : BaseModel
{
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}