using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab03.Models;

namespace lab03
{
    public interface IUsersService
    {
        public bool AddUser(User user);
        public User? GetById(int id);
        public List<User> GetAll();
        public bool Update(User user);
        public bool Delete(int id);
    }
}
