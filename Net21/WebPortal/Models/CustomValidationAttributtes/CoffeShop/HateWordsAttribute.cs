using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.CustomValidationAttributtes.CoffeShop
{
    public class HateWordsAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return $"Please do not use Duck in field {name}";
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
                throw new Exception("Attribute [HateWords] can work only strings");
            }

            return !text.Contains("Duck");
        }


    }
}
