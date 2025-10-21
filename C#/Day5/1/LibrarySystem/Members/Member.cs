using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Members
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfJoining { get; set; }
        public String[] BooksTaken { get; set; }

        public Member() {
            Console.WriteLine("members");

        }
    }
}
