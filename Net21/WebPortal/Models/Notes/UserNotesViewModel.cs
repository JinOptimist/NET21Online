using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.Notes;

public class UserNotesViewModel
{
    [Required(ErrorMessage = "UserName is required")]
    [IsUniqueUserName(ErrorMessage = "This UserName is already taken")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    [IsCorrectUrl(ErrorMessage = "Must be a valid image URL (.jpg, .png, .gif)")]
    public string AvatarUrl { get; set; }
    [Required(ErrorMessage = "Money is required")]
    public int Money { get; set; }
}