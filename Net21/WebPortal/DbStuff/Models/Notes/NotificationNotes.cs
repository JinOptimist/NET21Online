namespace WebPortal.DbStuff.Models.Notes;

public class NotificationNotes : BaseModel
{
    public string Message { get; set; }
        
    public DateTime CreateAt { get; set; }

    public virtual List<User> UserWhoViewedIt {  get; set; }

    public virtual User Author { get; set; }
}