using Core.Attributes;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.DTOS.Vacancy
{
    public class MangeVacancyDto : IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "MaxApplications must be grater than 0")]
        public int MaxApplications { get; set; }

        [Required]
        [DateOnly(ErrorMessage = "Date format must be DD/MM/YYYY")]
        public string ExpiryDate { get; set; }

        public int EmployerId { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!DateTimeOffset.TryParseExact(ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var expiryDate))
            {
                yield return new ValidationResult("Invalid ExpiryDate format. Use dd/MM/yyyy.", new[] { nameof(ExpiryDate) });
            }
            else if (expiryDate < DateTimeOffset.Now.Date)
            {
                yield return new ValidationResult("ExpiryDate must be greater than or equal to today's date.", new[] { nameof(ExpiryDate) });
            }
        }

    }
}
