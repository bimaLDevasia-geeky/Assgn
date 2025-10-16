using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3_c_
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; } 

        public Student(string name ,int Age) { 
            this.Name = name;
            this.Age = Age;
        }
        public void Display()
        {
            Console.WriteLine($"The Name is {Name}");
            Console.WriteLine($"The Age is {Age}");
        }
    }
}
