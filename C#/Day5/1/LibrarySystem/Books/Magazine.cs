using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Books
{
    public class Magazine
    {
        public int Id { set; get; }
        public string? Name { set; get; }
        public string? Description { set; get; } 

        public Magazine() {
            Console.WriteLine("This is a Magazine");
        }
       
    }
}
