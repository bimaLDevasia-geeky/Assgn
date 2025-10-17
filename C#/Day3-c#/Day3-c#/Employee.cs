using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Day3_c_
{
    public class Employee
    {
        public String Name {  get; set; }
        public long Salary { get; set; }

        

        public Employee(string Name) { 
            this.Name = Name;
        }

        public Employee(String Name,long Salary):this(Name) 
        { 
            this.Salary = Salary;
        }

        public void Display()
        {
            Console.WriteLine($"The Name is {Name}");
            Console.WriteLine("The Salary is "+ Salary);
        }
    }
}
