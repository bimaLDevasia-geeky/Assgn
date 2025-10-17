using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3_c_
{
    public class Swapper
    {
        public  static void Swap( ref int x,ref int y) {
            int temp;
            temp = x;
            x = y;
            y = temp;
        }
    }
}
