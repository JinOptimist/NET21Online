using System.ComponentModel.DataAnnotations;
using WebPortal.Models.Girls;

namespace WebPortal.Models.CustomValidationAttributtes
{
    public class IsNiceEnoughtAttribute : ValidationAttribute
    {
        private List<string> _coolNames = new List<string>()
        {
            "Nice",
            "Cool",
            "Smile"
        };
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            var viewModel = validationContext.ObjectInstance as GirlCreationViewModel;
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (viewModel.Rating > 8)
            {
                if (_coolNames.Any(coolWord => viewModel.Name!.Contains(coolWord)))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Too cool name for so low rating");
                }
                // not valid
            }

            // not valid
            return ValidationResult.Success;
        }
    }
}
