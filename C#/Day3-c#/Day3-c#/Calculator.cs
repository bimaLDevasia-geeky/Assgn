using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3_c_
{
    public class Calculator
    {
        public Calculator(int a,int b, string op)
        {
            switch (op) {
                case "+":
                    Console.WriteLine($"{a} + {b} => {a+b}");
                    break;
                case "-":
                    Console.WriteLine($"{a} - {b} => {a - b}");
                    break;
                case "*":
                    Console.WriteLine($"{a} * {b} => {a * b}");
                    break;
                case "/":
                    Console.WriteLine($"{a} / {b} => {a / b}");
                    break;
                case "%":
                    Console.WriteLine($"{a} % {b} => {a % b}");
                    break;
            }

        }
    }
}
