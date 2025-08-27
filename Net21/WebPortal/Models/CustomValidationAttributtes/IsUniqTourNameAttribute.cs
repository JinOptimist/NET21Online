using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.Tourism;

namespace WebPortal.Models.CustomValidationAttributtes
{
    public class IsUniqTourNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            var toursRepository = validationContext.GetRequiredService<IToursRepository>();
            var name = value as string;

            if (!toursRepository.IsUniqName(name))
            {
                return new ValidationResult("Is not uniq name");
            }

            return ValidationResult.Success;

        }
    }
}
