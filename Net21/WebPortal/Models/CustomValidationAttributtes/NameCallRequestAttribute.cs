using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebPortal.Models.CustomValidationAttributtes;

public class NameCallRequestAttribute : ValidationAttribute
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
        
        if (text.Length < 2 || text.Length > 50)
        {
            ErrorMessage = "Имя должно содержать от 2 до 50 символов";
            return false;
        }
        
        if (!Regex.IsMatch(text, @"^[a-zA-Zа-яА-ЯёЁ\s-]+$"))
        {
            ErrorMessage = "Имя может содержать только буквы, пробелы и дефис";
            return false;
        }

        return true;
    }
}