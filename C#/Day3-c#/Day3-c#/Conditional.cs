using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3_c_
{
    public class Conditional
    {
        public Conditional(int a ,int b)
        {
            Console.WriteLine("relational Operator");

            Console.WriteLine($"{a} > {b} -> {a>b}");
            Console.WriteLine($"{a} < {b} -> {a<b}");
            Console.WriteLine($"{a} == {b} -> {a == b}");
            Console.WriteLine($"{a} != {b} -> {a != b}");
            Console.WriteLine($"{a} >= {b} -> {a >= b}");
            Console.WriteLine($"{a} <= {b} -> {a <= b}");

            Console.WriteLine("Logical Operator");

            Console.WriteLine($"{a} > 10 AND {b} > 10 -> {a > 10 && b > 10}");
            Console.WriteLine($"{a} > 20 OR {b} < 10 -> {a > 20 || b < 10}");
            Console.WriteLine($" first digit is not equal to 5 -> {!(a==5)}");
        }
    }
}
