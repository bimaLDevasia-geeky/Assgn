// See https://aka.ms/new-console-template for more information


using EmployeeManagement;


Employee emp1 = new Employee("John Doe", 15000,Employee.EmployeeTypes.Permanent);
Employee emp2 = new Employee("Liam Smith", 20000, Employee.EmployeeTypes.Contract);
Employee emp3 = new Employee("Mary James", 15000,Employee.EmployeeTypes.Permanent);

Console.WriteLine("The Details \n");
emp1.PrintDetails();
emp2.PrintDetails();
emp3.PrintDetails();

Console.WriteLine($"The number of employees :  {Employee.NumberOfEmployee()}");
Console.WriteLine($"The next  employee Id available : {Employee.NextEmployeeId()}");