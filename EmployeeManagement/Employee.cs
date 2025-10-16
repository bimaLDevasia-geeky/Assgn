using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    public class Employee
    {
        public  enum EmployeeTypes
        {
            Permanent,
            Contract
        }

        static int Counter; 
        public  string Id { get; private set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public EmployeeTypes EmployeeType { get; set; }


        static Employee()
        {
            Counter = 1000;
            
        }

        public Employee( string name, double salary, EmployeeTypes emtype)
        {
            Id = "Emp" + Counter;
            this.Name = name;
            this.Salary = salary;
            this.EmployeeType = emtype;
            Counter++;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"{Id} {Name} {Salary} {EmployeeType} \n");
          
        }

        public static int NumberOfEmployee()
        {
            return Counter-1000;
        }

        public static int NextEmployeeId() {
            return Counter;
        }
    }

}