using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.CustomValidationAttributtes
{
    
    public class HateAppleAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"We like Android. Please do not use Apple in field {name}"
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            var text = value as string;
            if (text == null)
            {
                throw new Exception("Attribute [HateApple] can work only with string");
            }

            return !text.Contains("Apple");
        }
    }
}
