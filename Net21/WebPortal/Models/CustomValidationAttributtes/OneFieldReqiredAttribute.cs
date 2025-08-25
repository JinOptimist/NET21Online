using System.ComponentModel.DataAnnotations;
using WebPortal.Models.Tourism;

namespace WebPortal.Models.CustomValidationAttributtes
{
    public class OneFieldReqiredAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            var viewModel = validationContext.ObjectInstance as ShopViewModel;
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (viewModel.Author is null && viewModel.AuthorId is null)
            {
                return new ValidationResult($"New Author field should be entered or select existing author in dropdown");

            }
            else if (viewModel.Author is not null && viewModel.AuthorId is not null) 
            {
                return new ValidationResult($"Only Author field should be enterede or author in dropdown should be selected");
            }
            return ValidationResult.Success;

        }
    }
}
