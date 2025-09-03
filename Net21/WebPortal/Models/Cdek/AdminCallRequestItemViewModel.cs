using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPortal.Models.Cdek;

public class AdminCallRequestItemViewModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Question { get; set; }
    
    public string Status { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string AuthorName { get; set; }
    
    public int AuthorId { get; set; }
    public bool CanDelete { get; set; }
}