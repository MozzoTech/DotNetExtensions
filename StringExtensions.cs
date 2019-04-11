namespace Humair.Extensions
{
public static class StringExtensions
{
        public static bool is_valid_integer(this string value)
        {
            int out_value = 0;
            if (int.TryParse(value, out out_value))
            {
                return true;
            }
            return false;
        }
        public static bool is_valid_short(this string value)
        {
            short out_value = 0;
            if (short.TryParse(value, out out_value))
            {
                return true;
            }
            return false;
        }
        public static bool is_valid_byte(this string value)
        {
            byte out_value = 0;
            if (byte.TryParse(value, out out_value))
            {
                return true;
            }
            return false;
        }
        public static bool is_valid_double(this string value)
        {
            double out_value = 0;
            if (double.TryParse(value, out out_value))
            {
                return true;
            }
            return false;
        }
        public static List<int> TryParseInt(this string csv)
        {
            List<int> list_value = new List<int>();
            foreach (string str in csv.Split(',').Select(item => item))
            {
                int value;
                //value = int.TryParse(str, out value) ? value : (int?)null;
                if (int.TryParse(str, out value))
                {
                    list_value.Add(value);
                }
            }

            return list_value;
        }
        public static int TryParseStringToInt(this string string_value)
        {
            int value = 0;
            if (int.TryParse(string_value, out value))
            {
                return value;
            }
            return value;
        }
        public static bool TryParseStringToBool(this string string_value)
        {
            bool value = false;
            if (bool.TryParse(string_value, out value))
            {
                return value;
            }
            return value;
        }
        public static StringBuilder AppendFormatQueryString(this StringBuilder obj, string format, params object[] args)
        {
            if (obj.Length > 0)
            {
                obj.Append("&");
            }
            obj.AppendFormat(format, args);
            return obj;
        }
        public static string CombinePaths(this string obj, params string[] values)
        {
            StringBuilder result = new StringBuilder();

            foreach (string value in values)
            {
                if (!String.IsNullOrEmpty(result.ToString()) && !String.IsNullOrEmpty(value))
                    result.Append("/");

                result.Append(value);
            }
            return result.ToString();
        }
        public static bool is_null_or_whitespace(this string value)
        {
            return String.IsNullOrEmpty(value);
        }
        public static string ToNAIfIsNullOrWhitespace(this string value)
        {
            return value.is_null_or_whitespace() ? "NA" : value;
        }

        public static bool is_not_null_nor_whitespace(this string value)
        {
            return !String.IsNullOrEmpty(value);
        }

        public static string ToStringEmptyIfNull(this string value)
        {
            return value.is_null() ? string.Empty : value;
        }
        public static bool is_empty(this string value)
        {
            return String.IsNullOrEmpty(value);
        }

        public static string value_or_empty(this string value)
        {
            return value == null ? string.Empty : value;
        }

        public static string get_non_null_value(this string value)
        {
            return String.Format("{0}", value);
        }
        public static bool ContainsValues(this string first_csv_string, string second_csv_string)
        {
            int matched_count = (
                from first_item in first_csv_string.Split(',').Select(p => p.Trim())
                join second_item in second_csv_string.Split(',').Select(p => p.Trim())
                on first_item equals second_item
                select first_item
                    ).Count();

            if (matched_count > 0) { return true; }
            return false;
        }

        public static bool ContainsAny(this string value, string match_against_csv_string)
        {
            string[] match_against_list = match_against_csv_string.Split(',');
            foreach (string to_match_against in match_against_list)
            {
                if (value.Contains(to_match_against))
                    return true;
            }

            return false;
        }
        public static bool ContainsCaseInsensitive(this string source, string value)
        {
            int results = source.IndexOf(value, StringComparison.CurrentCultureIgnoreCase);

            return results == -1 ? false : true;
        }
        public static bool StartsWithCaseInsensitive(this string source, string value)
        {
            bool results = source.StartsWith(value, StringComparison.CurrentCultureIgnoreCase);

            return results;
        }

        public static bool EqualsCaseInsensitive(this string source, string value)
        {
            bool results = source.Equals(value, StringComparison.CurrentCultureIgnoreCase);

            return results;
        }
        public static int CompareToCaseInsensitive(this string source, string value)
        {
            int results = String.Compare(source, value, StringComparison.CurrentCultureIgnoreCase);

            return results;
        }
        public static string TruncateToWords(this string text, int no_of_words)
        {
            if (string.IsNullOrEmpty(text) || no_of_words == 0)
                return string.Empty;

            string truncatedtext = text;
            string[] words = text.Split(' ');

            if (words.Count() > no_of_words)
                truncatedtext = string.Join(" ", words.Take(no_of_words)) + "...";

            return truncatedtext;
        }
        public static string TruncateToCharacters(this string text, int no_of_characters)
        {
            if (string.IsNullOrEmpty(text) || no_of_characters == 0)
                return string.Empty;

            if (text.Count() <= no_of_characters)
                return text;

            return text.Substring(0, no_of_characters) + "...";
        }

        public static string TruncateCharactersToNearestWord(this string text, int no_of_characters)
        {
            if (string.IsNullOrEmpty(text) || no_of_characters == 0)
                return string.Empty;

            if (text.Count() <= no_of_characters)
                return text;
            int last_space = 0;
            for (int k = 0; k < no_of_characters; k++)
            {
                last_space = text[k] == ' ' ? k : last_space;
            }
            return text.Substring(0, last_space) + "...";
        }
        public static string escape_comma_with_space(this string text)
        {
            return text.Replace(',', ' ');
        }

        public static string either_or(this string text, string the_or_text)
        {
            if ((text != null) && (text.Length > 0))
                return text;
            else
                return the_or_text;
        }

        public static string either_or_untitled(this string text, string the_or_text)
        {
            if (((text != null) && (text.Length > 0)) && text.ToLower() != "untitled")
                return text;
            else
                return the_or_text;
        }
        public static string this_or_that(this string text, bool condition, string the_this_text, string the_that_text)
        {
            if (condition)
                return the_this_text;
            else
                return the_that_text;
        }
        public static string append_a_or_an(this string text)
        {
            if (text.StartsWithCaseInsensitive("a") || text.StartsWithCaseInsensitive("e") || text.StartsWithCaseInsensitive("i") || text.StartsWithCaseInsensitive("o") || text.StartsWithCaseInsensitive("u"))
                return "an " + text;
            else
                return "a " + text;
        }

        public static string append(this string text, string text_to_append)
        {
            return text + text_to_append;
        }
        public static IEnumerable<int> StringToIntList(this string str)
        {
            if (String.IsNullOrEmpty(str))
                yield break;

            foreach (var s in str.Split(','))
            {
                int num;
                if (int.TryParse(s, out num))
                    yield return num;
            }
        }

        public static IEnumerable<string> CsvToStringList(this string str)
        {
            if (String.IsNullOrEmpty(str))
                yield break;

            foreach (var s in str.Split(','))
            {
                yield return s;
            }
        }
        public static string add_protocol_to_link_if_needed(this string _link)
        {
            Regex protocol_sub_reg = new Regex("^https?://(www.)?");
            return (protocol_sub_reg.IsMatch(_link)) ? _link : "http://" + _link;
        }

        public static string encode_to_utf8(this string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            return Encoding.UTF8.GetString(bytes);
        }

}
}
