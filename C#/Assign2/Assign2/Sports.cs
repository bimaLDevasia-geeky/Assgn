using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign2
{
    public class Sports
    {
        public static void Check(string ch)
        {
            switch (ch) {
                case "c":
                    Console.WriteLine("Sports Name -> Cricket");
                    break;
                case "f":
                    Console.WriteLine("Sports Name -> Football");
                    break;
                case "h":
                    Console.WriteLine("Sports Name -> Hockey");
                    break;
                case "t":
                    Console.WriteLine("Sports Name ->Tennis");
                    break;
                case "b":
                    Console.WriteLine("Sports Name ->Badminton");
                    break;
                default: Console.WriteLine("invalid input");break;
            }
        }
    }
}
