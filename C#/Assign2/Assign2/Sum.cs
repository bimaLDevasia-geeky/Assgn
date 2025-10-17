using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assign2
{
    public class Sum
    {
        public void SumOfDigits(int val)
        {
            int sum = 0;

            while (val > 0)
            {
                int digit = val % 10;
                sum += digit;
                val /= 10;
            }
            Console.WriteLine(sum);
        }
    }
}
