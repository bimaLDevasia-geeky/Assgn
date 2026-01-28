using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    public class Vehicle
    {
        public virtual void ShowType() 
        {
            Console.Write("This is a Vehicle\n");
        }
    }

    public class Car:Vehicle 
    {
        public new void  ShowType()
        {
            Console.Write("This is a Car\n");
        }
    }
}
