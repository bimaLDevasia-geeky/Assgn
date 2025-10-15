using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign2
{
    public class Reverse
    {
        public void ReverseInteger(int val ) {
            int rev = 0; 
            while(val > 0 )
            {
                int last = val % 10;
                rev = rev * 10 + last;
                val = val / 10;
            }
            Console.WriteLine(rev);
        }
    }
}
