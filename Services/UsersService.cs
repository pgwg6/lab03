using lab03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        private readonly string ClaimTypeID = @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        private readonly string ClaimTypeName = @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
        private readonly string ClaimTypeSurname = @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
        private readonly string ClaimTypeEMail = @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

        public User? AddUser(ClaimsPrincipal user)
        {
            // NOTE : Hackish way to parse Google's id to internal id
            var idStr = user.Claims.FirstOrDefault(c => c.Type == ClaimTypeID).Value.TakeLast(9).ToArray();
            bool parsed = int.TryParse(idStr, out int id);

            if (!parsed)
                return null;

            var u = GetById(id);
            if (u != null)
                return u;

            u = new User();
            u.ID = id;
            u.Name = user.Claims.FirstOrDefault(c => c.Type == ClaimTypeName).Value;
            u.Surname = user.Claims.FirstOrDefault(c => c.Type == ClaimTypeSurname).Value;
            u.EMail = user.Claims.FirstOrDefault(c => c.Type == ClaimTypeEMail).Value;

            users.Add(u);
            return u;
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
