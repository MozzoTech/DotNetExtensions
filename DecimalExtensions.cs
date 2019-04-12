using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humair.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToDecimalPlaces(this decimal value, int no_of_decimal_places)
        {
            return String.Format("{0:N" + no_of_decimal_places.ToString() + "}", value);
        }

        public static string ToTwoDecimalPlaces(this decimal value)
        {
            return String.Format("{0:N2}", value);
        }

        public static string ToThousandSeparatorWithTwoDecimal(this decimal str)
        {
            return String.Format("{0:n}", str); //Output: 1,234.00
        }
        public static string ToThousandSeparatorWithoutDecimal(this decimal str)
        {
            return string.Format("{0:n0}", str); // no digits after the decimal point. Output: 9,876
        }
    }
}
