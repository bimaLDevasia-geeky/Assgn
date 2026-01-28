// See https://aka.ms/new-console-template for more information

using MenuBased;

EmployeeCollection employeeCollection = new EmployeeCollection();

Console.WriteLine("Welcome to the Employee Management System…");
bool exit = false;

while (!exit)
{

    Console.WriteLine("\nPlease choose one of the following:");
    Console.WriteLine("1. Add Employee");
    Console.WriteLine("2. Remove Employee");
    Console.WriteLine("3. Display All Employees");
    Console.WriteLine("4. Search Employee by Name");
    Console.WriteLine("5. Exit");
    Console.Write("Enter your choice (1-5): ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            HandleAddEmployee();
            break;
        case "2":
            HandleRemoveEmployee();
            break;
        case "3":
            employeeCollection.DisplayAllEmployees();
            break;
        case "4":
            HandleSearchEmployee();
            break;
        case "5":
            exit = true;
            Console.WriteLine("\nThank you for using the system. Exiting...");
            break;
        default:
            Console.WriteLine("\nInvalid choice. Please enter a number between 1 and 5.");
            break;
    }
}

 void HandleAddEmployee()
{
    Console.WriteLine("\n--- Add New Employee ---");

  
    Console.Write("Enter Name: ");
    string name = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(name))
    {
        Console.Write("Name cannot be empty. Please enter Name: ");
        name = Console.ReadLine();
    }

    double salary;
    Console.Write("Enter Salary: ");
    while (!double.TryParse(Console.ReadLine(), out salary) || salary < 0)
    {
        Console.Write("Invalid input. Please enter a valid positive Salary: ");
    }


    string employeeType;
    while (true)
    {
        Console.Write("Enter Employee Type (Permanent / Contract): ");
        string input = Console.ReadLine();
        if (input.Equals("Permanent", StringComparison.OrdinalIgnoreCase))
        {
            employeeType = "Permanent";
            break;
        }
        else if (input.Equals("Contract", StringComparison.OrdinalIgnoreCase))
        {
            employeeType = "Contract";
            break;
        }
        else
        {
            Console.WriteLine("Invalid type. Please enter 'Permanent' or 'Contract'.");
        }
    }

    employeeCollection.AddEmployee(name, salary, employeeType);
}


  void HandleRemoveEmployee()
{
    Console.Write("\nEnter Employee ID to remove: ");
    string id = Console.ReadLine();
    employeeCollection.RemoveEmployee(id);
}


 void HandleSearchEmployee()
{
    Console.Write("\nEnter Employee Name to search for: ");
    string name = Console.ReadLine();
    employeeCollection.SearchEmployeeByName(name);
}