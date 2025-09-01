using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.Models.CustomValidationAttributtes.CoffeShop
{
    public class IsUniqNameUser : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value, 
            ValidationContext validationContext)
        {
            var userCoffeShop = validationContext.GetRequiredService<IUserCommentRepository>();
            var name = value as string;

             if (!userCoffeShop.IsUniqNameCoffeUser(name))
             {
                return new ValidationResult("Is not uniq name");
             }

            return ValidationResult.Success;
        }

    }
}
