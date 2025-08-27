using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebPortal.Models.CustomValidationAttributtes;

public class PhoneCallRequestAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var phone = value as string;
        
        if (string.IsNullOrWhiteSpace(phone))
        {
            ErrorMessage = "Номер телефона обязателен";
            return false;
        }
        
        phone = Regex.Replace(phone, @"[\s\-\(\)]", "");
        
        // Проверяем: номер должен содержать только цифры и может начинаться с +
        if (!Regex.IsMatch(phone, @"^\+?\d{10,15}$"))
        {
            ErrorMessage = "Номер телефона должен содержать от 10 до 15 цифр и может начинаться с +";
            return false;
        }

        return true;
    }
}