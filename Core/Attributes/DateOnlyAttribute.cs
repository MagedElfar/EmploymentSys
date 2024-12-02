using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Core.Attributes
{
    public class DateOnlyAttribute : ValidationAttribute
    {
        private const string DateFormat = "dd/MM/yyyy"; // Correct date format 
        private const string DefaultErrorMessage = "Invalid date format. Use DD/MM/YYYY.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            // Allow null or empty values to be valid
            if (value == null || (value is string strValue && string.IsNullOrWhiteSpace(strValue)))
            {
                return ValidationResult.Success; // Skip validation if not provided
            }

            // Check if the value is a string
            if (value.ToString() is string dateString)
            {
                // Try to parse the string date using the specified format
                if (DateTimeOffset.TryParseExact(dateString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    return ValidationResult.Success; // Valid format
                }
            }

            // Return an error message if validation fails
            return new ValidationResult(ErrorMessage ?? DefaultErrorMessage);
        }
    }
}
