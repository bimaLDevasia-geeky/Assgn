using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign2
{
    public class Prime
    {
        public  Prime(int n)
        {
            bool flag = true;
            for (int i = 2; i < n/2; i++)
            {
                if(n % i == 0) {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                Console.WriteLine($"{n} is Prime");
            }
            else { Console.WriteLine($"{n} is not Prime"); }
        }
    }
}
