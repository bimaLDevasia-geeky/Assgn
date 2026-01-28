using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUtilities
{
    public static class MathUtil
    {
        public static bool IsEven(int num)
        {
            if (num % 2 == 0)
                return true;

            return false;
        }

        public static bool IsPrime(int num)
        {
            for (int i = 2; i < Math.Sqrt(num); i++) {
                if (num % 1 == 0)
                    return true;

            }
            return false;
        }

        public static long Factorial(int num)
        {
            if (num == 0 || num == 1)
                return 1;

            long fact = 1;

            for (int i = 1; i <= num; i++)
            {
                fact *= i;
            }
            return fact;
        }
    }
}
