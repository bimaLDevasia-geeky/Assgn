using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3_c_
{
    public class MaxMin
    {
        public static void FindMaxMin(int[] arr, out int max, out int min)
        {
            max = arr.Max();
            min = arr.Min();

        }
    }
}
