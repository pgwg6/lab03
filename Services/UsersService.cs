using lab03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab03
{
    public class UsersService : IUsersService
    {
        // NOTE : Hardcoded list
        private readonly List<User> users = new List<User>
        {
            new User{ ID = 0, Name = "Jon", Surname = "Doe", EMail = "JonDoe@aggr.io" },
            new User{ ID = 1, Name = "Abe", Surname = "Ben", EMail = "AbeBen@aggr.io" },
            new User{ ID = 2, Name = "Kab", Surname = "Ete", EMail = "KabEte@aggr.io" },
            new User{ ID = 3, Name = "San", Surname = "Pai", EMail = "SanPai@aggr.io" },
            new User{ ID = 4, Name = "Tai", Surname = "Gem", EMail = "TaiGem@aggr.io" },
            new User{ ID = 5, Name = "Ken", Surname = "Nat", EMail = "KenNat@aggr.io" }
        };
        public bool AddUser(User user)
        {
            if(GetById(user.ID) != null)
                return false;

            users.Add(user);
            return true;
        }

        public bool Delete(int id)
        {
            if (GetById(id) == null)
                return false;

            users.RemoveAll(u => u.ID == id);
            return true;
        }

        public List<User> GetAll()
        {
            return users;
        }

        public User? GetById(int id)
        {
            return users.FirstOrDefault(u => u.ID == id);
        }

        public bool Update(User user)
        {
            var u = GetById(user.ID);
            if (u == null)
                return false;

            u.ID = user.ID;
            u.Name = user.Name;
            u.Surname = user.Surname;
            u.EMail = user.EMail;
            return true;
        }
    }
}
