using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.Marketplace
{
    public class ProductTypeRequiredAttribute : ValidationAttribute
    {
        private string RequiredProductType { get; set; }

        public ProductTypeRequiredAttribute(string requiredProductType)
        {
            RequiredProductType = requiredProductType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var instance = context.ObjectInstance;
            var type = instance.GetType();

            var productTypeProperty = type.GetProperty("ProductType");
            if (productTypeProperty == null)
            {
                return ValidationResult.Success;
            }

            var productTypeValue = productTypeProperty.GetValue(instance, null)?.ToString();

            if (productTypeValue == RequiredProductType && (value == null || string.IsNullOrEmpty(value.ToString())))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}