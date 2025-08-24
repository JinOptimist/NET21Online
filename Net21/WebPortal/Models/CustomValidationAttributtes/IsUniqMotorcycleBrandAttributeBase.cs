using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.Models.CustomValidationAttributtes
{
    public class IsUniqMotorcycleBrandAttributeBase : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var motorcycleTypeRepositories = validationContext.GetRequiredService<IMotorcycleBrandRepositories>();
            var brand = value as string;
            if (!motorcycleTypeRepositories.IsUniqBrand(brand))
            {
                return new ValidationResult("Is not uniq type");
            }
            return ValidationResult.Success;
        }
    }
}