using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomObject
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }

        public Book(string title,string author,double salary) { 
            this.Title = title;
            this.Author = author;
            this.Price = salary;
        }

        public void DisplayBook()
        {
            Console.WriteLine();
            Console.Write($"The title is {this.Title} \t ");
            Console.Write("The Author is "+ this.Author+"\t");
            Console.Write("The Price is "+this.Price+"\t");
            Console.WriteLine("\n");
        }
    }
}
