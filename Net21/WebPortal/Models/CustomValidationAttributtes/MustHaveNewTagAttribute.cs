using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;

namespace WebPortal.Models.CustomValidationAttributtes;

public class MustHaveNewTagAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var tagIds = value as List<int>;
        if (tagIds == null || tagIds.Count == 0)
        {
            return new ValidationResult("Note must have at least one tag");
        }

        var tagRepository = validationContext.GetService<ITagRepository>();
        if (tagRepository == null)
        {
            return new ValidationResult("Tag repository is not available");
        }

        try
        {
            var selectedTagNames = tagRepository.GetTagNamesByIds(tagIds);

            if (!selectedTagNames.Any(name => name.Equals("#NEW", StringComparison.OrdinalIgnoreCase)))
            {
                return new ValidationResult("Note must contain the #NEW tag");
            }
        }
        catch (Exception ex)
        {
            return new ValidationResult("Error validating tags");
        }

        return ValidationResult.Success;
    }
}