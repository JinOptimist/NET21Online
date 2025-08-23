using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.Models.CustomValidationAttributtes
{
    public class AntiBotAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                throw new Exception("Message can't be null");
            }
            if (value is not string)
            {
                throw new Exception("Message can't be not string type of");
            }

            var commentsRepository = validationContext.GetRequiredService<ICommentRepository>();

            var message = value as string;

            var allMessages = commentsRepository.GetAll().Select(c => c.Message).ToList();

            if (allMessages.Contains(message!))
            {
                return new ValidationResult("You can't send comments with same message");
            }

            return ValidationResult.Success;
        }
    }
}
