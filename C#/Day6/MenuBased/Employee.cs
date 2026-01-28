using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBased
{
    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public string EmployeeType { get; set; } 

        
        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Salary: ${Salary:F2}, Type: {EmployeeType}";
        }


    }
}
