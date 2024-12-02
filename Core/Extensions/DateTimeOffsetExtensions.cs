using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static DateTimeOffset? ParseStringDate(this DateTimeOffset value , string? date)
        {

            if(string.IsNullOrEmpty(date))
                return null;


            DateTimeOffset? paredDate = null;


            if (DateTimeOffset.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var tempDate))
            {
                paredDate = tempDate;
            }

            return paredDate;
        }

        public static DateTimeOffset? ParseStringDateToEndOfDay(this DateTimeOffset value, string? date)
        {

            if (string.IsNullOrEmpty(date))
                return null;


            DateTimeOffset? paredDate = null;


            if (DateTimeOffset.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var tempDate))
            {
                paredDate = tempDate.Date.AddDays(1).AddTicks(-1);
            }

            return paredDate;
        }
    }
}
