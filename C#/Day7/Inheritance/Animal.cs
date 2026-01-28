using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    public class Animal
    {
        public virtual void Speak()
        {
            Console.Write("An Animal can Speak \n");
        }
    }

    public class Dog : Animal
    {
        public override void Speak()
        {
            Console.Write("The Dog says woof \n");
        }
    }

    public class Cat:Animal { 
        public override void Speak() {
            Console.Write("The Cat says Meow\n");
        }
    }

}
