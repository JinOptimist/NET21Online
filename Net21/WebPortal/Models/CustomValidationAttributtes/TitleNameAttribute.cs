using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.CustomValidationAttributtes
{
    public class TitleNameAttribute : ValidationAttribute
    {
        private string _title = "tour";

        public TitleNameAttribute()
        {
            _title = "tour";
        }
        public TitleNameAttribute(string title)
        {
            _title = title;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Plaease use word {_title} in title name"
                : ErrorMessage;
        }
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            var text = value as string;
            if (text == null)
            {
                throw new Exception("Attribute [Title Name] can work only with string");
            }

            return text.Contains(_title, StringComparison.OrdinalIgnoreCase);

        }
    }
}
