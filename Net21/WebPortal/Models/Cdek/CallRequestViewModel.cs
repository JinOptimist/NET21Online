using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.Cdek;

public class CallRequestViewModel
{
    [NameCallRequest]
    public string Name { get; set; }

    [PhoneCallRequest]
    public string PhoneNumber { get; set; }

    [QuestionCallRequest]
    public string Question { get; set; }
}