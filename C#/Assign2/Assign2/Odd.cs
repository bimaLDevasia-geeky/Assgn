using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign2
{
    public class Odd
    {
        public void FindOdd()
        {
            int i = 1;
            Console.WriteLine("The Odd Numbers are: ");
            while (i <= 50)
            {
                if(i%2 != 0)
                {
                    
                    Console.Write($"{i} ");
                }
                i++;
            }
        }
    }
}
