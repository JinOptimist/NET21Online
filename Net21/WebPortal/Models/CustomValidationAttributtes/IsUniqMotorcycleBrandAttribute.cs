using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.Models.CustomValidationAttributtes
{
    public class IsUniqMotorcycleBrandAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var motorcycleBrandRepositories = validationContext.GetRequiredService<IMotorcycleBrandRepositories>();
            var brand = value as string;
            if (!motorcycleBrandRepositories.IsUniqBrand(brand))
            {
                return new ValidationResult("Is not uniq brand");
            }
            return ValidationResult.Success;
        }
    }
}
