using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.CustomValidationAttributtes;

public class QuestionCallRequestAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            ErrorMessage = "Поле обязательно для заполнения";
            return false;
        }
        
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