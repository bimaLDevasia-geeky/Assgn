using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3_c_
{
    internal class Swap
    {
        public Swap(int a ,int b) {
            Console.WriteLine($"Before Swapping first:{a} second:{b} ");
            a = a + b;
            b = a - b;
            a = a - b;
            Console.WriteLine($"After Swapping first:{a} second:{b}");
        }   
    }
}
