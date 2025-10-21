using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class User
    {
        static int _counter;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsActive { get; set; }


        static User()
        {
            _counter = 1000;
        }

        public User(string name,string email,string password ,bool isactive) 
        {
            Id = _counter;
            Name = name;
            Email = email;
            Password = password;
            IsActive = isactive;
            _counter++;
        }

    }
}
