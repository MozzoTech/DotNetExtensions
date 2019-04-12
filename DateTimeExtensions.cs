using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humair.Extensions
{

    public static class DateTimeExtensions
    {
        public static string ToRelativeDateOffset(this DateTime datetime)
        {
            int offset_hours = System.TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours * -1;
            int offset_minutes = System.TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Minutes * -1;

            return datetime.AddHours(offset_hours).AddMinutes(offset_minutes).ToRelativeDate();

        }

        public static string ToGroupRelativeDate(this DateTime input)
        {

            TimeSpan oSpan = DateTime.Now.Subtract(input.ToLocalTime());
            double TotalMinutes = oSpan.TotalMinutes;
            string Suffix = " ago";

            if (TotalMinutes < 0.0)
            {
                TotalMinutes = Math.Abs(TotalMinutes);
                //Suffix = " from now";
            }

            Dictionary<double, Func<string>> aValue = new Dictionary<double, Func<string>>();
            aValue.Add(0.75, () => "less than a minute");
            aValue.Add(1.5, () => "a minute");
            aValue.Add(45, () => string.Format("{0} minutes", Math.Round(TotalMinutes)));
            aValue.Add(90, () => "an hour");
            aValue.Add(1440, () => string.Format("{0} hours", Math.Round(Math.Abs(oSpan.TotalHours)))); // 60 * 24
            aValue.Add(2880, () => "a day"); // 60 * 48
            aValue.Add(43200, () => string.Format("{0} days", Math.Floor(Math.Abs(oSpan.TotalDays)))); // 60 * 24 * 30
            aValue.Add(86400, () => "a month"); // 60 * 24 * 60
            aValue.Add(525600, () => string.Format("{0} months", Math.Floor(Math.Abs(oSpan.TotalDays / 30)))); // 60 * 24 * 365 
            aValue.Add(1051200, () => "a year"); // 60 * 24 * 365 * 2
            aValue.Add(double.MaxValue, () => string.Format("{0} years", Math.Floor(Math.Abs(oSpan.TotalDays / 365))));

            return aValue.First(n => TotalMinutes < n.Key).Value.Invoke() + Suffix;
        }

        public static DateTimeOffset GetDateTimeOffsetUTC(this DateTime value)
        {
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime m_scheduled_date_in_Utc = TimeZoneInfo.ConvertTimeToUtc(value, est);
            var utcOffset = new DateTimeOffset(m_scheduled_date_in_Utc, TimeSpan.Zero);
            return utcOffset;
        }

        public static String GetTimestamp(this DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        public static String GetTimestamp(this DateTime value, string separator)
        {
            return String.Format("{0}_{1}_{2}", value.ToString("yyyyMMdd"), separator, value.ToString("HHmmssffff"));
        }

        public static string to_relative_date(this DateTime date_time)
        {
            date_time = date_time.ToLocalTime();

            var timeSpan = DateTime.Now - date_time;

            // span is less than 60 seconds, measure in seconds.
            if (timeSpan < TimeSpan.FromMinutes(1))
            {
                return timeSpan.Seconds + " seconds ago";
            }

            // span is less than 60 minutes, measure in minutes.
            if (timeSpan < TimeSpan.FromHours(1))
            {
                return timeSpan.Minutes > 1 ? "about " + timeSpan.Minutes + " minutes ago" : "a minute ago";
            }

            // span is less than 24 hours, measure in hours.
            if (timeSpan < TimeSpan.FromDays(1))
            {
                return timeSpan.Hours > 1 ? "about " + timeSpan.Hours + " hours ago" : "about an hour ago";
            }

            // span is less than 30 days (1 month), measure in days.
            if (timeSpan < TimeSpan.FromDays(30))
            {
                return timeSpan.Days > 1 ? "about " + timeSpan.Days + " days ago" : "about a day ago";
            }

            // span is less than or equal to 365 days (1 year), measure in months.
            if (timeSpan < TimeSpan.FromDays(365))
            {
                return (timeSpan.Days / 30) > 1 ? "about " + timeSpan.Days / 30 + " months ago" : "about a month ago";
            }

            // span is greater than 365 days (1 year), measure in years.
            return (timeSpan.Days / 365) > 1 ? "about " + timeSpan.Days / 365 + " years ago" : "about a year ago";

        }

        public static long to_utc(this DateTime date_time)
        {
            return (long)date_time.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        public static DateTime from_utc(this DateTime date_time, long utc)
        {
            return new DateTime(1970, 1, 1).AddMilliseconds(utc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToRelativeDate(this DateTime input)
        {

            TimeSpan oSpan = DateTime.Now.Subtract(input.ToLocalTime());
            double TotalMinutes = oSpan.TotalMinutes;
            string Suffix = " ago";

            if (TotalMinutes < 0.0)
            {
                TotalMinutes = Math.Abs(TotalMinutes);
                //Suffix = " from now";
            }

            Dictionary<double, Func<string>> aValue = new Dictionary<double, Func<string>>();
            aValue.Add(0.75, () => "less than a minute");
            aValue.Add(1.5, () => "about a minute");
            aValue.Add(45, () => string.Format("{0} minutes", Math.Round(TotalMinutes)));
            aValue.Add(90, () => "about an hour");
            aValue.Add(1440, () => string.Format("about {0} hours", Math.Round(Math.Abs(oSpan.TotalHours)))); // 60 * 24
            aValue.Add(2880, () => "a day"); // 60 * 48
            aValue.Add(43200, () => string.Format("{0} days", Math.Floor(Math.Abs(oSpan.TotalDays)))); // 60 * 24 * 30
            aValue.Add(86400, () => "about a month"); // 60 * 24 * 60
            aValue.Add(525600, () => string.Format("{0} months", Math.Floor(Math.Abs(oSpan.TotalDays / 30)))); // 60 * 24 * 365 
            aValue.Add(1051200, () => "about a year"); // 60 * 24 * 365 * 2
            aValue.Add(double.MaxValue, () => string.Format("{0} years", Math.Floor(Math.Abs(oSpan.TotalDays / 365))));

            return aValue.First(n => TotalMinutes < n.Key).Value.Invoke() + Suffix;
        }

        public static string ToRelativeString(this TimeSpan ts)
        {
            if (ts == TimeSpan.MaxValue || ts == TimeSpan.MinValue) return "Sometime";

            StringBuilder sb = new StringBuilder();



            double years = ts.Days < 365.0 ? 0 : (ts.Days / 365.0);

            if ((int)ts.Days == 365.0 || (int)ts.Days == 366.0) sb.Append("1 year, ");
            else if (ts.Days > 365.0) sb.Append((int)(ts.Days / 365.0) + " years, ");
            else if (ts.Days == 1.0) sb.Append("1 day, ");
            else if (ts.Days > 1.0) sb.Append((int)ts.Days + " days, ");

            if (ts.Hours == 1.0) sb.Append("1 hour, ");
            else if (ts.Hours > 1.0) sb.Append((int)ts.Hours + " hours, ");

            if (ts.Minutes == 1.0) sb.Append("1 minute, ");
            else if (ts.Minutes > 1.0) sb.Append((int)ts.Minutes + " minutes, ");

            if (ts.Seconds == 1.0) sb.Append("1 second, ");
            else if (ts.Seconds > 1.0) sb.Append((int)ts.Seconds + " seconds, ");

            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }       

        public static string ToProperDateString(this DateTime date)
        {
            return date.ToString("MMM dd, yyyy");
        }

        public static string ToProperDateString(this DateTime date, bool ReturnMinValue)
        {
            return ReturnMinValue ? date.ToString("MMM dd, yyyy") : date == DateTime.MinValue ? string.Empty : date.ToString("MMM dd, yyyy");
        }

        public static string ToProperDateTimeString(this DateTime date)
        {
            return date.ToString("MMM dd, yyyy hh:mm:ss tt");
        }

        public static string ToProperTimeString(this DateTime date)
        {
            return date.ToString("hh:mm:ss tt");
        }

        public static string TO_MM_DD_YY(this DateTime date)
        {
            return date.ToString("MM/dd/yyyy");
        }
        public static string TO_HH_MM_TT(this DateTime date)
        {
            return date.ToString("hh:mm tt");
        }

        public static string TO_DD_MMMM_yyyy(this DateTime date)
        {
            return date.ToString("dd MMMM, yyyy");
        }

        public static string TO_DD_MMMM_yyyy(this DateTime date, bool ReturnMinValue)
        {
            return ReturnMinValue ? date.TO_DD_MMMM_yyyy() : date == DateTime.MinValue ? string.Empty : date.TO_DD_MMMM_yyyy();
        }
        public static string TO_HH_MM_TT(this TimeSpan time_span)
        {
            DateTime dt = new DateTime(2014, 1, 1);
            dt = dt + time_span;
            return dt.TO_HH_MM_TT();
        }


        public static DateTime FirstDayOfMonthFromDateTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime LastDayOfMonthFromDateTime(this DateTime dateTime)
        {
            DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

        public static string ToMonthDayAndTime(this DateTime date)
        {
            return date.ToString("M/dd h:mm tt");
        }

        public static string ToMonthDayAndYearIfNotCurrent(this DateTime date, bool full_month = false)
        {
            DateTime current_year = DateTime.Now;
            String _month = full_month ? "MMMM" : "MMM";
            return current_year.ToString("yyyy") != date.ToString("yyyy") ? date.ToString(_month + " dd, yyyy") : date.ToString(_month + " dd");
        }

        public static bool is_not_null(this Nullable<DateTime> value)
        {
            if (value.HasValue
                && (value.Value != DateTime.MaxValue || value.Value != DateTime.MinValue))
            {
                return true;
            }
            return false;
        }

        public static string GetIconFromMonth(this DateTime date)
        {
            string result = "";
            switch (date.ToString("MMM"))
            {
                case "Jan":
                    result = "!";
                    break;
                case "Feb":
                    result = "@";
                    break;
                case "Mar":
                    result = "#";
                    break;
                case "Apr":
                    result = "$";
                    break;
                case "May":
                    result = "%";
                    break;
                case "Jun":
                    result = "^";
                    break;
                case "Jul":
                    result = "&";
                    break;
                case "Aug":
                    result = "*";
                    break;
                case "Sep":
                    result = "(";
                    break;
                case "Oct":
                    result = ")";
                    break;
                case "Nov":
                    result = "_";
                    break;
                case "Dec":
                    result = "+";
                    break;
                default:
                    result = "not implemented";
                    break;
            }
            return result;

        }
        public static string GetIconFromDay(this DateTime date)
        {
            string result = "";
            switch (date.ToString("dd"))
            {
                case "01":
                    result = "1";
                    break;
                case "02":
                    result = "2";
                    break;
                case "03":
                    result = "3";
                    break;
                case "04":
                    result = "4";
                    break;
                case "05":
                    result = "5";
                    break;
                case "06":
                    result = "6";
                    break;
                case "07":
                    result = "7";
                    break;
                case "08":
                    result = "8";
                    break;
                case "09":
                    result = "9";
                    break;
                case "10":
                    result = "0";
                    break;
                case "11":
                    result = "a";
                    break;
                case "12":
                    result = "b";
                    break;
                case "13":
                    result = "c";
                    break;
                case "14":
                    result = "d";
                    break;
                case "15":
                    result = "e";
                    break;
                case "16":
                    result = "f";
                    break;
                case "17":
                    result = "g";
                    break;
                case "18":
                    result = "h";
                    break;
                case "19":
                    result = "i";
                    break;
                case "20":
                    result = "j";
                    break;
                case "21":
                    result = "k";
                    break;
                case "22":
                    result = "l";
                    break;
                case "23":
                    result = "m";
                    break;
                case "24":
                    result = "n";
                    break;
                case "25":
                    result = "o";
                    break;
                case "26":
                    result = "p";
                    break;
                case "27":
                    result = "q";
                    break;
                case "28":
                    result = "r";
                    break;
                case "29":
                    result = "s";
                    break;
                case "30":
                    result = "t";
                    break;
                case "31":
                    result = "u";
                    break;

                default:
                    result = "not implemented";
                    break;

            }
            return result;
        }

        public static string YearMonthPrefix(this DateTime date)
        {
            return String.Format("{0}/{1}/", DateTime.Now.Year, DateTime.Now.Month);
        }
        public static string ToDatePickerFormattedDate(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public static string ToDatePickerFormattedDate(this DateTime date, bool ReturnMinValue)
        {
            return ReturnMinValue ? date.ToString("dd/MM/yyyy") : date == DateTime.MinValue ? string.Empty : date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); 
        }

        public static string ToMMMM_yyyy(this DateTime date)
        {
            return date.ToString("MMMM, yyyy");
        }

        public static string ToMMMM_yyyy(this DateTime date, bool ReturnMinValue)
        {
            return ReturnMinValue ? date.ToString("MMMM, yyyy") : date == DateTime.MinValue ? string.Empty : date.ToString("MMMM, yyyy");
        }
    }
}
