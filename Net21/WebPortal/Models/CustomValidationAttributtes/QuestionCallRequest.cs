using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.CustomValidationAttributtes;

public class QuestionCallRequest : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var text = value as string;
        if (text == null)
        {
            throw new Exception("Поле должно быть строкой");
        }
        
        if (string.IsNullOrWhiteSpace(text))
        {
            ErrorMessage = "Поле обязательно для заполнения";
            return false;
        }

        return true;
    }
}