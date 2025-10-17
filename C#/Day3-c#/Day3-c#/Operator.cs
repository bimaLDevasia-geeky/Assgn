using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Day3_c_
{
    public class Operator
    {
        public Operator()
        {
            Console.WriteLine("Enter the first number");
            string val = Console.ReadLine();
            int? number = Utilities.ConvertToInt(val);

            Console.WriteLine("Enter the second Number");
            string val2  = Console.ReadLine();
            int? num2 = Utilities.ConvertToInt(val2);

            int first, second;
            if (number.HasValue && num2.HasValue )
            {
                first=number.Value;
                second=num2.Value;
            }
            else
            {
                Console.WriteLine("Invalid input");
                return;
            }

            Console.Write($"The sum is {first+second} \n");
            Console.WriteLine($"The Diff is {first-second}");
            Console.WriteLine($"The product is {first * second}");
            Console.WriteLine($"The quotient is {first / second}");
            Console.WriteLine($"The remainder is {first % second}");



        }
    }
}
