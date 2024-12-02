using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Core.Attributes
{
    public class EnumStringValidationAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        public EnumStringValidationAttribute(Type enumType)
        {
            _enumType = enumType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Allow null or empty strings as valid (optional)
            if (string.IsNullOrEmpty(value as string))
            {
                return ValidationResult.Success; // Skip validation if not provided
            }

            if (value is string enumStringValue)
            {
                // Check if the provided value is a valid enum name
                if (Enum.IsDefined(_enumType, enumStringValue))
                {
                    return ValidationResult.Success;
                }
            }

            // List valid values in the error message
            var validValues = string.Join(", ", Enum.GetNames(_enumType));
            var errorMessage = $"Status: Invalid status. Valid values are: {validValues}.";

            return new ValidationResult(errorMessage);
        }
    }
}
