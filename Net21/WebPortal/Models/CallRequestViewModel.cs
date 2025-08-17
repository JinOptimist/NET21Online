namespace WebPortal.Models;

public class CallRequestViewModel
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? Question { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public DateTime CreationTime { get; set; }
}