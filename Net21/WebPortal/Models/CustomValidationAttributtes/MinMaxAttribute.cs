using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.CustomValidationAttributtes
{
    public class MinMaxAttribute : ValidationAttribute
    {
        private int _min = 0;
        private int _max = 10;

        public MinMaxAttribute()
        {
            _min = 0;
            _max = 10;
        }

        public MinMaxAttribute(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Value of {name} must be between {_min} and {_max}"
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is not int)
            {
                throw new Exception("Attribute [MinMax] can work only with int");
            }

            var number = (int)value;

            return number >= _min && number <= _max;
        }
    }
}
