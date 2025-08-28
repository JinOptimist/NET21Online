using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.Cdek;

public class CallRequestViewModel
{
    
    [Required (ErrorMessage = "Как мы можем к Вам обращаться?")]
    [NameCallRequest]
    public string Name { get; set; }

    [Required (ErrorMessage = "Укажите, пожалуйста, номер для оперативной связи. Конфиденциально.")]
    [PhoneCallRequest]
    public string PhoneNumber { get; set; }

    [Required (ErrorMessage = "Опишите, пожалуйста, кратко суть Вашего вопроса.")]
    [QuestionCallRequest]
    public string Question { get; set; }
}