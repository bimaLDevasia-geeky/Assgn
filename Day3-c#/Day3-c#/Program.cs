
using Day3_c_;
using System;
using System.Globalization;

//first

Console.WriteLine("Input two numbers and perform all arithmetic operations");

Operator op = new Operator();


Console.WriteLine("\n");

//second
Console.WriteLine("Check whether a number is even or odd using the ternary operator");

Console.Write("Enter the number : ");
String ternary = Console.ReadLine();
int val;

int.TryParse(ternary, out val);

Ternary ter = new Ternary(val);


Console.WriteLine("\n");

//third

Console.WriteLine("Compare two integers using relational and logical operators.");

Console.WriteLine("Enter the first number");
String numb1 = Console.ReadLine();


Console.WriteLine("Enter the second number");
String numb2 = Console.ReadLine();



if(int.TryParse(numb1, out int val1) && int.TryParse(numb2,out int val2))
{
    Conditional con = new Conditional(val1, val2);
}
else
{
    Console.WriteLine("Invalid input");
}

Console.WriteLine("\n");



//fourth

Console.WriteLine("Swap two numbers without using a third variable (use arithmetic)");

Console.WriteLine("Enter the first number");
numb1 = Console.ReadLine();

Console.WriteLine("Enter the second number");
numb2 = Console.ReadLine();

if (int.TryParse(numb1, out val1 ) && int.TryParse(numb2, out val2 ))
{
    Swap con = new Swap(val1, val2);
}
else
{
    Console.WriteLine("Invalid input");
}

Console.WriteLine("\n");

//fifth
Console.WriteLine("Check if a number lies between 10 and 50 using logical operators");
Console.WriteLine("Enter the number");
numb1 = Console.ReadLine();



if (int.TryParse(numb1, out val1))
{
    Between b = new Between(val1);
}
else
{
    Console.WriteLine("Invalid input");
}


//sixth
Console.WriteLine("Simulate a simple calculator using a switch statement and arithmetic operators.");
Console.WriteLine("Enter the first number");
numb1 = Console.ReadLine();

Console.WriteLine("enter anyone operator +,-,/,%,*");
string opera = Console.ReadLine();

Console.WriteLine("Enter the second number");
numb2 = Console.ReadLine();



if (int.TryParse(numb1, out val1) && int.TryParse(numb2, out val2))
{
    Calculator con = new Calculator(val1, val2, opera);
}
else
{
    Console.WriteLine("Invalid input");
}

Console.WriteLine("\n");

//7th

Console.WriteLine("Create a class Student with fields Name and Age. Add a constructor to initialize them and display details in a separate method .");

Console.WriteLine("Enter the Name of the Student ");

string name = Console.ReadLine();

Console.WriteLine("Enter the Age of the Student ");
numb1 = Console.ReadLine();

if(int.TryParse(numb1,out val1))
{
    Student st = new Student(name, val1);
    Console.WriteLine("Details");
    st.Display();
}


Console.WriteLine("\n");

//8th
Console.WriteLine("Create a class Employee with two constructors (Name only; Name, Salary) using constructor chaining.");
Console.WriteLine("Enter the Name");
name = Console.ReadLine();

Console.WriteLine("Enter the Salary");
numb1 = Console.ReadLine();

if(int.TryParse(numb1,out val1))
{
    Employee employee = new Employee(name, val1);
    employee.Display();
}


Console.WriteLine("\n");


//9th
Console.WriteLine("Create a class BankAccount(AccountNumber, AccountHolder, Balance ) with default and parameterized constructors using constructor chaining. Add Deposit() which increments the balance and DisplayBalance() to display the account details methods.");

Console.WriteLine("Enter the AccountNumber ");

numb1 = Console.ReadLine();

Console.WriteLine("Enter the AccountHolder");
name = Console.ReadLine();


Console.WriteLine("Enter the amount to deposist");
numb2 = Console.ReadLine();



if(int.TryParse(numb1,out val1 )  && int.TryParse(numb2,out val2))
{
    Accunt user = new Accunt(val1,name);
    user.AddDeposit(val2);
    user.DisplayDetails();

}

Console.WriteLine("\n");

//10th 
Console.WriteLine("Swap two integers using the ref keyword");

Console.Write("Enter two number seperated with space :");
string str = Console.ReadLine();

string[] arr = str.Split(" ");

if (int.TryParse(arr[0],out val1) && int.TryParse(arr[1],out val2))
{
Console.WriteLine("Before Swapping"+" "+ val1 + " "+val2);
    Swapper.Swap( ref val1, ref val2);
    Console.WriteLine("After Swapping"+" "+ val1 +" " +val2);

}

Console.WriteLine("\n");
//11th

Console.WriteLine("Write a method GetSumAndAverage(int a, int b, out int sum, out double avg) that returns sum and average using out parameters.");

Console.Write("Enter two number seperated with space :");
str = Console.ReadLine();

arr = str.Split(" ");
int sum;
double avg;

if ( arr.Length==2 && (int.TryParse(arr[0],out val1) && int.TryParse(arr[1],out val2)))
{
    SumAndAverage.GetSumAndAverage(val1,val2,out sum,out avg);
    Console.WriteLine($"The sum is {sum} and the average is {avg} ");
}

Console.WriteLine("\n");

//last 
Console.WriteLine("Write a method FindMaxMin(int[] arr, out int max, out int min) that finds maximum and minimum using out");

Console.WriteLine("Enter the numbers of the array seperated by spaces");

str= Console.ReadLine();

arr = str.Split(" ");

List<int> nums = new List<int>();
foreach(string s in arr)
{
    if (int.TryParse(s,out val1))
        nums.Add(val1);
}

int[] numArr = nums.ToArray();


MaxMin.FindMaxMin(numArr,out int max,out int min);

Console.WriteLine($"The max is {max} and min is {min}");