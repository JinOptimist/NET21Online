using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebPortal.Models.CustomValidationAttributtes;

public class IsCorrectImageUrlAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string url)
        {
            return new ValidationResult("Invalid url value type");
        }

        var relativePattern = @"^([a-zA-Z0-9_\-/]+)\.(jpg|jpeg|png|gif)$";
        var absolutePattern = @"^(https?:\/\/[^\s]+)\.(jpg|jpeg|png|gif)$";

        if (Regex.IsMatch(url, relativePattern, RegexOptions.IgnoreCase) ||
            Regex.IsMatch(url, absolutePattern, RegexOptions.IgnoreCase))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage ?? "Invalid image URL format");
    }
}