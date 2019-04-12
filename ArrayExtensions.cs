using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humair.Extensions
{
   
    public static class ArrayExtensions
    {
        public static bool ContainsEnum(this Array @array, int value)
        {
            bool return_val = false;

            for (int index = 0; index < @array.Length; index++)
            {
                int index_value = (int)@array.GetValue(index);
                return_val = (index_value == value);
                if (return_val) break;
            }

            return return_val;

        }

        public static bool ContainsEnum(this Array @array, byte value)
        {
            bool return_val = false;

            for (int index = 0; index < @array.Length; index++)
            {
                int index_value = (int)@array.GetValue(index);
                return_val = (index_value == value);
                if (return_val) break;
            }

            return return_val;

        }
    }
}
