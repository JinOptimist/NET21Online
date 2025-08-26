using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.Models.CustomValidationAttributtes
{
    public class IsUniqTitleNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            var spaceStationRepository = validationContext.GetRequiredService<ISpaceStationRepository>();
            var title = value as string;

            if (!spaceStationRepository.IsUniqTitle(title))
            {
                return new ValidationResult("Is not uniq Title");
            }

            return ValidationResult.Success;
        }
    }
}
