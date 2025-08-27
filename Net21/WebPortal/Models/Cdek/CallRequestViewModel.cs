using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.Cdek;

public class CallRequestViewModel
{
    [NameCallRequest]
    [Required]
    public string Name { get; set; }

    [PhoneCallRequest]
    [Required]
    public string PhoneNumber { get; set; }

    [QuestionCallRequest]
    [Required]
    public string Question { get; set; }
}