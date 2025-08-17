namespace WebPortal.DbStuff.Models;

public class CallRequest : BaseModel
{
    public string Name { get; set; }
    
    public string Question { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public DateTime CreationTime { get; set; }
}