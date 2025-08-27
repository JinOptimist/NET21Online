namespace WebPortal.DbStuff.Models.Notes;

public class User : BaseModel
{
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string AvatarUrl { get; set; }
    public int Money { get; set; }

    public virtual List<Note> CreatedNotes { get; set; } = new List<Note>();
    public virtual List<Note> FavoriteNotes { get; set; } = new List<Note>();
}
