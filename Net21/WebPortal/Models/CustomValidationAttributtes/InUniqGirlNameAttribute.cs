using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.Models.CustomValidationAttributtes
{
    public class InUniqGirlNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            var girlRepository = validationContext.GetRequiredService<IGirlRepository>();
            var name = value as string;

            if (!girlRepository.IsUniqName(name))
            {
                return new ValidationResult("Is not uniq name");
            }

            return ValidationResult.Success;
        }
    }
}
