using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBased
{
    public class EmployeeCollection
    {
        private List<Employee> employees;
        private static int idCounter;

        public EmployeeCollection()
        {
            employees = new List<Employee>();
            idCounter = 1000;
        }


        public void AddEmployee(string name, double salary, string employeeType)
        {

            string newId = "Emp" + idCounter;
            idCounter++;


            Employee newEmployee = new Employee
            {
                Id = newId,
                Name = name,
                Salary = salary,
                EmployeeType = employeeType
            };


            employees.Add(newEmployee);
            Console.WriteLine($"\nEmployee added successfully! New Employee ID: {newId}");
        }

        public void RemoveEmployee(string id)
        {
            
            Employee empToRemove = employees.FirstOrDefault(e => e.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

            if (empToRemove != null)
            {
                employees.Remove(empToRemove);
                Console.WriteLine($"\nEmployee with ID {id} has been removed.");
            }
            else
            {
                
                Console.WriteLine($"\nNo Employee found with Id {id}");
            }
        }

        
        public void DisplayAllEmployees()
        {
            Console.WriteLine("\n--- All Employees ---");
            if (employees.Count == 0)
            {
                Console.WriteLine("There are no employees in the system.");
            }
            else
            {
                foreach (var emp in employees)
                {
                    Console.WriteLine(emp);
                }
            }
            Console.WriteLine("---------------------");
        }

        
        public void SearchEmployeeByName(string name)
        {
            
            List<Employee> foundEmployees = employees.Where(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (foundEmployees.Count > 0)
            {
                Console.WriteLine($"\n--- Found {foundEmployees.Count} Employee(s) with name '{name}' ---");
                foreach (var emp in foundEmployees)
                {
                    Console.WriteLine(emp);
                }
            }
            else
            {
                
                Console.WriteLine($"\nNo Employee found with name {name}");
            }
        }
    }
}
