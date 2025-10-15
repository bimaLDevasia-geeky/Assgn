using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign2
{
    public class Tax
    {
        public static void CalculateTax(int amount) { 
            if (amount <=10000)
            {
                Console.WriteLine("The tax is 5%");
            }
            else if(amount > 10000 && amount <= 15000)
                Console.WriteLine("The Tax is 7.5%");
            else if(amount > 15000 && amount <= 20000)
                Console.WriteLine("The tax is 10%");
            else if(amount > 20000 && amount < 25000)
                Console.WriteLine("The tax is 12.5%");
            else
                Console.WriteLine("The tax is 15%");
        }
    }
}
