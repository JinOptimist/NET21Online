using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Repositories.CompShop;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.Models.CustomValidationAttributtes.CompShop
{
    public class InUniqCompShopNameAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? "Это не уникальное имя"
                : ErrorMessage;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var deviceRepository = validationContext.GetRequiredService<DeviceRepository>();

            var name = value as string;

            if (!deviceRepository.IsUniqName(name))
            {
                return new ValidationResult("Это не уникальное имя");
            }

            return ValidationResult.Success;
        }
    }
}
