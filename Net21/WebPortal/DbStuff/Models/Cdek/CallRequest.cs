namespace WebPortal.DbStuff.Models;

public class CallRequest : BaseModel
{
    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public string Question { get; set; }

    public string Status { get; set; } = "Новая";

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime CreationTime { get; set; }
    
    public virtual User? Author { get; set; }
}