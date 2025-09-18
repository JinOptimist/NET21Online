using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Localizations;
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
                return new ValidationResult(TourismLoc.Validation_Uniq_Name);
            }

            return ValidationResult.Success;

        }
    }
}
