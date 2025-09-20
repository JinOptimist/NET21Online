namespace WebPortal.DbStuff.Models;

public class CdekChat : BaseModel
{
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual List<User> UserWhoViewedIt { get; set; } = new();
    public virtual User Author { get; set; }
}