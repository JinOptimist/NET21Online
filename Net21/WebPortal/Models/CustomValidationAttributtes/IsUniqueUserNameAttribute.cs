using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;

namespace WebPortal.Models.CustomValidationAttributtes;

public class IsUniqueUserNameAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        var userNotesRepository = validationContext.GetRequiredService<IUserNotesRepository>();

        if (userNotesRepository == null)
        {
            throw new InvalidOperationException("Repository not available");
        }

        var userName = value as string;
        if (!userNotesRepository.IsUniqUserName(userName!))
        {
            return new ValidationResult(ErrorMessage ?? "UserName must be unique");
        }

        return ValidationResult.Success;
    }
}