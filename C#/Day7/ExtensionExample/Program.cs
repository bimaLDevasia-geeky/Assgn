// See https://aka.ms/new-console-template for more information
using ExtensionExample;


Console.Write("Enter the sentence to convert into Title case : ");
   string text = Console.ReadLine();

Console.WriteLine("Title Case : "+text.ToTitleCase());

Console.WriteLine("\n");


List<int> numbers = new List<int> { 10, 0, 20, 30, 0 };

Console.WriteLine("THe numbers");

foreach (int number in numbers)
    Console.Write(number+" ");

Console.WriteLine();
Console.WriteLine($"The average without zero is : { numbers.AverageExceptZero()}");
