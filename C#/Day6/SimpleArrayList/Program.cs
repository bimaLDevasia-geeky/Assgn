// See https://aka.ms/new-console-template for more information


using SimpleArrayList;
using System.Globalization;

SimpleArr Arr = new SimpleArr();

Console.WriteLine("Adding Names");
Arr.AddItems("John");
Arr.AddItems("Dark");
Arr.AddItems("Blah");
Arr.AddItems("KKK");
Arr.AddItems("bruh");

Console.WriteLine("\nItems");
Arr.Display();

Console.WriteLine("\nEnter the name to remove");
string name = Console.ReadLine();

Console.WriteLine($"\nRemoving {name} from list");
Arr.RemoveItem(name);


Console.WriteLine("\nEnter the name to add");
 name = Console.ReadLine();
Console.WriteLine("Enter the position");
string val = Console.ReadLine();
if (int.TryParse(val,out int pos))
{
    Arr.InsertAtPosition(name, pos);
}



Console.WriteLine("\n");

Arr.Display();


