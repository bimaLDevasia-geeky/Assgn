using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3_c_
{
    public   class SumAndAverage
    {
        public static void GetSumAndAverage(int a, int b, out int sum, out double avg)
        {
            sum = a + b;
            avg = (double)(a + b) / 2;
            
        }
    }
}
