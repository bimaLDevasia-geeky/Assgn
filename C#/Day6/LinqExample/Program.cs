// See https://aka.ms/new-console-template for more information

List<string> Names = new List<string>() { "Bimal","Amal","ABrosky","Hamlet","Casper","Jeorge" };

Console.WriteLine("Items:");
foreach (string name in Names)
{
    Console.WriteLine(name);
}
Console.WriteLine("\n");

Console.WriteLine("Name Starting with A");

 List<string> nameWithA =Names.Where(x => x.StartsWith("A")).ToList();

foreach (string name in nameWithA)
    Console.WriteLine(name);

Console.WriteLine("\n");



Console.WriteLine("names with length greater than 4");

List<string> nameGreaterThan4 = Names.Where(x => x.Length>4).ToList();

foreach(string name in nameGreaterThan4)
{ Console.WriteLine(name); }
