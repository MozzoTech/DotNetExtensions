namespace Humair.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToStringAsEmptyIfNull(this bool value)
        {
            if (value.is_null())
                return string.Empty;
            else
                return value.ToString();
        }

        public static bool is_null(this object value)
        {
            if (value == null)
            { return true; }
            else { return false; }
        }

        public static bool is_not_null(this object value)
        {
            return !value.is_null();
        }

        public static bool IsNull<T>(this T obj) where T : class
        {
            return obj == null;
        }
        public static bool IsNotNull<T>(this T obj) where T : class
        {
            return obj != null;
        }

        public static bool GetValue(this bool? val) 
        {
            return val.HasValue ? val.Value : false;
        }

    }
}
