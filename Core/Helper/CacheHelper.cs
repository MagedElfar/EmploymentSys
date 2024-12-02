using Core.DTOS.Vacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public static class CacheHelper
    {

        public static  string BuildCashKey(string key , object value)
        {
            return $"{key}:{value}";
        }
        public static string BuildQueryCashKey<T>(T queryDto , string mainKye)
        {
            var sb = new StringBuilder();

            sb.Append(mainKye);

            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {

                var value = prop.GetValue(queryDto);
                if (value != null)
                    sb.Append($":{prop.Name}:{value}");
            }


            return sb.ToString();
        }

    }
}
