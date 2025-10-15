
using Assign2;


static int? ConvertToInt(String? str)
{
    if(int.TryParse(str, out int i))
    {
        return i;
    }
    else
    {
        Console.WriteLine("Not in a good format");
        return null;
    }
}


//first

First20 first = new First20();
Console.WriteLine("Print first 20 numbers using for loop");
first.PrintFirst20();

Console.WriteLine("\n");

//second
Console.WriteLine("Print odd numbers less than 50 using while loop");
Odd odd = new Odd();
odd.FindOdd();

Console.WriteLine("\n");

//third
Console.WriteLine("Large amount 3 numbers");
Large large = new Large();

Console.Write("Enter the first Number:");
String val = Console.ReadLine();
int? firstVal =ConvertToInt(val);

Console.Write("Enter the second Number:");
val = Console.ReadLine();
int? secondVal = ConvertToInt(val);

Console.Write("Enter the third Number:");
val = Console.ReadLine();
int? thirdVal = ConvertToInt(val);

large.large(firstVal, secondVal, thirdVal);

Console.WriteLine("\n");

//fourth
Console.WriteLine("Reverse of a number");
Reverse reverse = new Reverse();
Console.WriteLine("Enter the number");

val = Console.ReadLine();

int? number = ConvertToInt(val);

if (number.HasValue)
{
reverse.ReverseInteger(number.Value);
}

Console.WriteLine("\n");

//fifth

Console.WriteLine("Sum of the digits of a number");
Sum sum = new Sum();
val = Console.ReadLine();

number = ConvertToInt(val);

if (number.HasValue)
{
    sum.SumOfDigits(number.Value);
}

Console.WriteLine("\n");


//6th 
Console.WriteLine("Check prime number");


Console.WriteLine("enter the number");
val = Console.ReadLine();
number = ConvertToInt(val);

if (number.HasValue)
{
    Prime p = new Prime(number.Value);
}

Console.WriteLine("\n");

//7th

Console.WriteLine("Print all prime numbers below 100");
Prime100 p2 = new Prime100();

Console.WriteLine("\n");

// Fibonacci
Fibonacci fibonacci = new Fibonacci();
Console.Write("ENter the number of Fibonoccie required:");
val = Console.ReadLine();
number = ConvertToInt(val);

if (number.HasValue)
{
    fibonacci.PrintFibonacci(number.Value);
}

Console.WriteLine("\n");

//Tax calculation program
Console.WriteLine("Enter the amount to check the tax");
val = Console.ReadLine();

number = ConvertToInt(val);

if (number.HasValue)
Tax.CalculateTax(number.Value);


Console.WriteLine("\n");

//Sports

Console.WriteLine("Enter the character to search for sports");

val = Console.ReadLine();

Sports.Check(val);
