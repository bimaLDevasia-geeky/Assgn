﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Members
{
    public class Librarian
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public  DateTime HireDate { get; set; }

        public Librarian() {
            Console.WriteLine("Libranian");
        }
    }
}
