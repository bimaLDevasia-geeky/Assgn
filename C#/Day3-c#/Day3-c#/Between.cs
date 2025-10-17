using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3_c_
{
    public class Between
    {
        public Between(int a) {
            if (a > 10 && a < 50)
                Console.WriteLine($"{a} is between 10 and 50");
            else
                Console.WriteLine($"{a} is not between 10 and 50");
        }
    }
}
