using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3_c_
{
    public static class Utilities
    {
        public static int? ConvertToInt(string str)
        {
            if (string.IsNullOrEmpty(str))  return null;

            if(int.TryParse(str, out int result))
            {
                return result;
            }
            else
            {
                return null;
            }

        }
    }
}
