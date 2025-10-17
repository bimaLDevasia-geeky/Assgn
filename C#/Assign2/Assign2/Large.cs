using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign2
{
    public class Large
    {
        public void large(int? a ,int? b, int? c)
        {
            if (a == null || b == null || c == null)
                return;

            if (a > b && a > c)
                Console.WriteLine($"{a} is the greatest");
            else if (b > a && b > c)
                Console.WriteLine($"{b} is the greatest");
            else
                Console.WriteLine($"{c} is the greatest");
        }
    }
}
