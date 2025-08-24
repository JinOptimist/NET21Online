using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.Notes;

public class TagViewModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
}