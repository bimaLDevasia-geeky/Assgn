using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{

    public class UserServices
    {
        private readonly UserRepo _userRepo;

        public UserServices()
        {
            _userRepo = new UserRepo();
        }

        public List<UserDetails> getActiveUser()
        {
            return _userRepo.GetUsers().Where(u=> u.IsActive == true).ToList();
        }

        public void AddUser(string name,string email,string password)
        {
            _userRepo.AddUser(name, email, password);
        }

    }
}
