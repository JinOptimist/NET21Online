using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.Notes;

public class UserNotesViewModel
{
    public int? Id { get; set; } = 0;
    public string? UserName { get; set; } = null;
}