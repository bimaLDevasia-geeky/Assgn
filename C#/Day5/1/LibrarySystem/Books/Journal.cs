using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Books
{
    public class Journal
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public Journal() {
            Console.WriteLine("This is a Journal");
        }
    }
}
