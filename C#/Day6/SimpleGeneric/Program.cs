// See https://aka.ms/new-console-template for more information
using System.Collections;

List<int> Marks = new List<int>();

Marks.Add(78);
Marks.Add(92);
Marks.Add(67);
Marks.Add(88);
Marks.Add(95);
Console.WriteLine("Items");
foreach (int i in Marks)
{
    Console.Write(i + " ");
}
Console.WriteLine();


Console.WriteLine($"The average is {(Marks.Sum()*1.0/Marks.Count)}\n");

Console.WriteLine($"removing the  lowest : {Marks.Min()}\n");
Marks.Remove(Marks.Min());

Console.WriteLine("Items");
foreach (int i in Marks)
{
    Console.Write(i + " ");
}
Console.WriteLine();

Console.WriteLine("Sorting\n");

Marks.Sort();

Console.WriteLine("Items");
foreach (int i in Marks)
{
    Console.Write(i+" ");
}
Console.WriteLine();

