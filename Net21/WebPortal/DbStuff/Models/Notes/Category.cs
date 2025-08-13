namespace WebPortal.DbStuff.Models.Notes;

public class Category : BaseModel
{
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}