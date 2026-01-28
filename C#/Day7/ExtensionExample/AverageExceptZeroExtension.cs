using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionExample
{
    public static  class AverageExceptZeroExtension
    {
        public static double AverageExceptZero(this List<int> input)
        {
            
            int count = input.Where(x => x != 0).Count();
            int sum = input.Where(x=>x!=0).Sum();

            return (sum*1.00)/count;
        }
    }
}
