using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab03.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace lab03
{
    public interface IUsersService
    {
        public bool AddUser(User user);
        public User? AddUser(ClaimsPrincipal user);
        public User? GetById(int id);
        public List<User> GetAll();
        public bool Update(User user);
        public bool Delete(int id);
    }
}
