using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Day3_c_
{
    public class Ternary
    {
        public Ternary(int a) {

            bool isEven= a%2==0? true : false;
            if(isEven)
                Console.WriteLine("Even");
            else
                Console.WriteLine("Odd");
        }
    }
}
