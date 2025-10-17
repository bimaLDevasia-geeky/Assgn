using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign2
{
    public class Prime100
    {
        public Prime100()
        {
            Console.WriteLine("the Prime Numbers below 100");
            for (int i = 2; i < 100; i++)
            {
                int flag = 0;
                for (int j = 2; j <= i/2; j++)
                {
                    if (i % j == 0)
                    {
                        flag=1;
                        break;
                    }
                 
                }
                if (flag == 0)
                {
                    Console.Write($"{i} ");
                }
            }
            
        }
    }
}
