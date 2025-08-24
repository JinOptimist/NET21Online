using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebPortal.Models.CustomValidationAttributtes;

public class PhoneCallRequest : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            ErrorMessage = "Номер телефона обязателен";
            return false;
        }
        
        if (value is not int number)
        {
            ErrorMessage = "Номер телефона должен быть числом";
            return false;
        }
        
        if (number <= 0)
        {
            ErrorMessage = "Номер телефона не может быть отрицательным или нулевым";
            return false;
        }
        
        var numberString = number.ToString();
        if (!Regex.IsMatch(numberString, @"^\d{10,15}$"))
        {
            ErrorMessage = "Номер телефона должен содержать от 10 до 15 цифр";
            return false;
        }

        return true;
    }
}