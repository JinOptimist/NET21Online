using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WebPortal.Models.CustomValidationAttributtes.Notes;

namespace WebPortal.Models.AuthNotes;

public class AuthNotesRegistrationViewModel
{
    [Required(ErrorMessage = "Username is required")]
    [IsUniqueUserName]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [ValidateNever] public string? ReturnUrl { get; set; }
}