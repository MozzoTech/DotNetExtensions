using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Humair.Extensions
{
   
    public static class ListExtensions
    {
        public static bool IsNull<T>(this T obj) where T : class
        {
            return obj == null;
        }
        public static bool IsAny<T>(this IEnumerable<T> data) where T : class
        {
            return data != null && data.Any();
        }
        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            var enumerator = items.GetEnumerator();
            return !enumerator.MoveNext();
        }
        public static T FirstOrNewDefault<T>(this List<T> list)// where T : new()
        {
            if (list.Count > 0)
                return list.FirstOrDefault<T>();
            else
                return Activator.CreateInstance<T>();
        }


        public static Tuple<T1, T2> FirstOrNewDefault<T1, T2>(this IEnumerable<Tuple<T1, T2>> enum_list)
        {
            if (enum_list.Count() > 0)
                return enum_list.FirstOrDefault<Tuple<T1, T2>>();
            else
            {
                return new Tuple<T1, T2>((T1)Activator.CreateInstance(typeof(T1)), (T2)Activator.CreateInstance(typeof(T2)));
            }
        }

        public static IDictionary<string, object> ToDictionary(this object obj)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
            foreach (PropertyDescriptor property in properties)
            {
                result.Add(property.Name, property.GetValue(obj));
            }
            return result;
        }

        public static IDictionary<string, object> AddProperty(this object obj, string name, object value)
        {
            var dictionary = obj.ToDictionary();
            dictionary.Add(name, value);
            return dictionary;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            T[] elements = source.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                int swapIndex = rng.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }


    }

    public class MatchComparer : IComparer<string>
    {
        private readonly string _searchTerm;

        public MatchComparer(string searchTerm)
        {
            _searchTerm = searchTerm;
        }

        public int Compare(string x, string y)
        {
            /*
             * -1 = first string is search term so must come first
             * 0 =  Both entries are equal                 * 
             * 1 = second string is search term so must come first
             */

            if (x.EqualsCaseInsensitive(y)) return 0; // Both entries are equal;
            if (x.EqualsCaseInsensitive(_searchTerm)) return -1; // first string is search term so must come first
            if (y.EqualsCaseInsensitive(_searchTerm)) return 1; // second string is search term so must come first
            if (x.StartsWithCaseInsensitive(_searchTerm))
            {
                // first string starts with search term
                // if second string also starts with search term sort alphabetically else first string first
                return (y.StartsWithCaseInsensitive(_searchTerm)) ? x.CompareTo(y) : -1;
            };
            if (y.StartsWithCaseInsensitive(_searchTerm)) return 1; // second string starts with search term so comes first
            if (x.ContainsCaseInsensitive(_searchTerm))
            {
                // first string contains search term
                // if second string also contains the search term sort alphabetically else first string first
                return (y.ContainsCaseInsensitive(_searchTerm)) ? x.CompareTo(y) : -1;
            }
            if (y.ContainsCaseInsensitive(_searchTerm)) return 1; // second string contains search term so comes first
            return x.CompareTo(y); // fall back on alphabetic
        }
    }

    public class MATCHSTARTSWITHCOMPARER : MATCHCOMPARER, IComparer<string>
    {
        public MATCHSTARTSWITHCOMPARER(string searchTerm)
            : base(searchTerm)
        {

        }
    }

    public abstract class MATCHCOMPARER
    {
        protected readonly string _searchTerm;

        public MATCHCOMPARER()
        {
        }

        public MATCHCOMPARER(string searchTerm)
        {
            _searchTerm = searchTerm;
        }

        public int Compare(string x, string y)
        {
            /*
             * -1 = first string is search term so must come first
             * 0 =  Both entries are equal                 * 
             * 1 = second string is search term so must come first
             */

            if (x.EqualsCaseInsensitive(y)) return 0; // Both entries are equal;
            if (x.EqualsCaseInsensitive(_searchTerm)) return -1; // first string is search term so must come first
            if (y.EqualsCaseInsensitive(_searchTerm)) return 1; // second string is search term so must come first
            if (x.StartsWithCaseInsensitive(_searchTerm))
            {
                // first string starts with search term
                // if second string also starts with search term sort alphabetically else first string first
                return (y.StartsWithCaseInsensitive(_searchTerm)) ? x.CompareTo(y) : -1;
            };
            if (y.StartsWithCaseInsensitive(_searchTerm)) return 1; // second string starts with search term so comes first
            if (x.IndexOf(_searchTerm, StringComparison.OrdinalIgnoreCase) < y.IndexOf(_searchTerm, StringComparison.OrdinalIgnoreCase)) { return -1; } // first string has index lesser search term so must come first
            if (x.IndexOf(_searchTerm, StringComparison.OrdinalIgnoreCase) == y.IndexOf(_searchTerm, StringComparison.OrdinalIgnoreCase)) { return 0; } // Both entries are equal;
            if (y.IndexOf(_searchTerm, StringComparison.OrdinalIgnoreCase) < x.IndexOf(_searchTerm, StringComparison.OrdinalIgnoreCase)) { return 1; }  // second string has index lesser search term so must come first
            if (x.ContainsCaseInsensitive(_searchTerm))
            {
                // first string contains search term
                // if second string also contains the search term sort alphabetically else first string first
                return (y.ContainsCaseInsensitive(_searchTerm)) ? x.CompareTo(y) : -1;
            }
            if (y.ContainsCaseInsensitive(_searchTerm)) return 1; // second string contains search term so comes first
            return x.CompareTo(y); // fall back on alphabetic
        }

    }
}
