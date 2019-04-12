using System.Globalization;

namespace Humair.Extensions
{
   
    public static class IntegerExtensions
    {
        public static int to_int(this int? obj)
        {
            return (obj.HasValue) ? obj.Value : 0;
        }

        public static string ToMonth(this int value, bool long_or_short = false)
        {
            return calculate_month(value, long_or_short);
        }

        public static string ToMonth(this byte value, bool long_or_short = false)
        {
            return calculate_month(value, long_or_short);
        }

        public static string ToMonth(this short value, bool long_or_short = false)
        {
            return calculate_month(value, long_or_short);
        }

        private static string calculate_month(object value, bool long_or_short)
        {
            string result = "";
            if (((int)value > 12) || ((int)value < 1))
                result = "out of range month";
            else
            {
                result = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((int)value);
            }

            return result;
        }

        public static string ToStringZeroAsEmpty(this int value)
        {
            if (value == 0)
                return "";
            else
                return value.ToString();
        }

        public static string ToStringAsEmptyIfNull(this int value)
        {
            if (value.is_null())
                return string.Empty;
            else
                return value.ToString();
        }

        public static string ToStringZeroAsEmpty(this short value)
        {
            int an_int = value;
            return an_int.ToStringZeroAsEmpty();
        }

        public static string ToStringZeroAsEmpty(this byte value)
        {
            int an_int = value;
            return an_int.ToStringZeroAsEmpty();
        }


    }
}
