using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;

namespace WebPortal.Models.CustomValidationAttributtes.Notes;

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
            throw new InvalidOperationException("Repository not available");
        }

        var selectedTagNames = tagRepository.GetTagNamesByIds(tagIds);

        return !selectedTagNames.Any(name => name.Equals("#NEW", StringComparison.OrdinalIgnoreCase)) 
            ? new ValidationResult("Note must contain the #NEW tag") 
            : ValidationResult.Success;
    }
}