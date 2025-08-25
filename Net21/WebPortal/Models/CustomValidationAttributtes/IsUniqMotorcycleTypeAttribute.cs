using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.Models.CustomValidationAttributtes
{
    public class IsUniqMotorcycleTypeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var motorcycleTypeRepositories = validationContext.GetRequiredService<IMotorcycleTypeRepositories>();
            var type = value as string;
            if (!motorcycleTypeRepositories.IsUniqType(type))
            {
                return new ValidationResult("Is not uniq type");
            }
            return ValidationResult.Success;
        }
    }
}
