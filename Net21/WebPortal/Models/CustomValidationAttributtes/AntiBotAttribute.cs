using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.Models.CustomValidationAttributtes
{
    public class AntiBotAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }

            if (value is not string)
            {
                throw new Exception("Message can't be not string type of");
            }

            var commentsRepository = validationContext.GetRequiredService<ICommentRepository>();

            var message = value as string;

            if (commentsRepository.GetByMessage(message!) != null)
            {
                return new ValidationResult("You can't send comments with same message");
            }
            return ValidationResult.Success;
        }
    }
}
