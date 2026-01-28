// See https://aka.ms/new-console-template for more information




using Calculator;
using System;
using System.Linq.Expressions;



while (true)
{
    Console.WriteLine("\nEnter the first number ");
    string num1 = Console.ReadLine();

    Console.WriteLine("Enter the second number");
    string num2 = Console.ReadLine();

    Console.WriteLine("Enter the operator ( + , - , * , / )");
    string opera = Console.ReadLine();

    PerformCal(num1, num2, opera);
}
 void PerformCal(string num1,string num2,string opera)
{
    Calculator.Calculator cal = new Calculator.Calculator();

    try
    {
        int a = int.Parse(num1);
        int b = int.Parse(num2);
        double res= 0;
        switch (opera)
        {
            case "+":
                res = cal.Add(a, b);
                break;
            case "-": 
                res = cal.Sub(a, b);
                break;
            case "*":
                res = cal.Mult(a, b);
                break;
            case "/":
                res = cal.Divide(a,b);
                break;
            default:
                Console.WriteLine("User Friendly Error: Invalid operation.");
                break;
        }
        Console.WriteLine($"The result is {res}");
    }
    catch (FormatException ex)
    {
        Console.WriteLine("One of the input is not a valid number \n" +"Message -> "+ ex.Message);
    }
    catch(OverflowException ex) {
        Console.WriteLine("There is an Overflow Exception \n" + "Message -> " + ex.Message);
            }
    catch(DivideByZeroException ex)
    {
        Console.WriteLine("You are trying to divide a number by zero\n" + "Message -> " + ex.Message);
    }
}