using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{

    public class UserDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
    public class UserRepo
    {
        static List<User> users = new List<User>
            {
                new User("John","john@gmail.com","johni123",true),
                new User("Ivern","ivern@gmail.com","ivern123",false),
                new User("Rengar","rengar@gmail.com","rengar123",true)
            };

        public List<UserDetails> GetUsers()
        {
           
            List<UserDetails> response = users.Select(u => new UserDetails
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                IsActive = u.IsActive
            }).ToList() ;

            return response;
            
        }

        public void AddUser(string name,string email,string password)
        {
            users.Add(new User(name, email, password, true));
        }
    }
}
