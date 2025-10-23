// See https://aka.ms/new-console-template for more information

using CustomObject;
using System.Collections;


List<Book> books = new List<Book>();

Book item1 = new Book("Harry Potter", "Larry olsen ", 2300);
books.Add(item1);
Book item2 = new Book("Life Of Pie", "Sreenivas Redyy", 1200);
books.Add(item2);
Book item3 = new Book("Jungle Book", "Charlie Dickens", 3300);
books.Add(item3);

Console.WriteLine("Displaying items");

foreach (Book book in books)
{
    book.DisplayBook();
}

Console.WriteLine("The book with the highest price.");

Book item = books.OrderByDescending(x=>x.Price).FirstOrDefault();

item.DisplayBook();

Console.WriteLine("Removing Items - Life of Pie");

item = books.FirstOrDefault(x=> x.Title.Equals("Life of Pie",StringComparison.OrdinalIgnoreCase));

if (item != null)
{
   
    books.Remove(item);
    Console.WriteLine("Book removed!");
}
else
{
   
    Console.WriteLine("Book not found, nothing removed.");
}


Console.WriteLine("Displaying items");

foreach (Book book in books)
{
    book.DisplayBook();
}

