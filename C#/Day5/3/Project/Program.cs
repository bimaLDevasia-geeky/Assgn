// See https://aka.ms/new-console-template for more information


using MathUtilities;

Console.WriteLine("Enter a Value");

string val = Console.ReadLine();

if(int.TryParse(val,out int res))
{
    if (MathUtil.IsEven(res))
        Console.WriteLine("The Number is Even");
    else
        Console.WriteLine("The Number is Odd");

    if (MathUtil.IsPrime(res))
        Console.WriteLine("The Number is Prime");
    else
        Console.WriteLine("The Number is Prime");

    Console.WriteLine($"Factorial of {res} is {MathUtil.Factorial(res)}");
}