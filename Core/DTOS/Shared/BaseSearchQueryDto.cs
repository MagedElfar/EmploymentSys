using Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Shared
{
    public class BaseSearchQueryDto : IValidatableObject
    {

        [DateOnly(ErrorMessage = "Date format must be DD/MM/YYYY")]
        public string? FromDate { get; set; }

        [DateOnly(ErrorMessage = "Date format must be DD/MM/YYYY")]
        public string? ToDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Limit should be more than 1")]
        public int? Limit { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page should be more than 1")]
        public int? Page { get; set; } = 1;

        public bool? Asc { get; set; } = true;



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string dateFormat = "dd/MM/yyyy"; // Correct date format

            if (
                DateTimeOffset.TryParseExact(FromDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset fromDate)
            &&
                DateTimeOffset.TryParseExact(ToDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset toDate)
            )
            {
                if (fromDate > toDate)
                {
                    yield return new ValidationResult(
                    "FromDate must be earlier than or equal to ToDate.",
                    new string[] { "Date Error" });
                }
            }
        }


    }
}
