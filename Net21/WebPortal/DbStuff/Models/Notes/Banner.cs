namespace WebPortal.DbStuff.Models.Notes;

public class Banner : BaseModel
{
    public string Name { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}