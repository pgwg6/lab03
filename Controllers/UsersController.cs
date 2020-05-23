using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lab03.Models;

namespace lab03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService _UsersService;
        public UsersController(IUsersService usersService)
        {
            _UsersService = usersService;
        }

        // POST: api/Users
        // BODY:
        //  {
        //      "id": 10,
        //      "name": "Abe",
        //      "surname": "Ben",
        //      "eMail": "AbeBen@aggr.io"
        //  }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Post(User user)
        {
            var result = _UsersService.AddUser(user);
            if (!result)
                return Conflict($"User with {user.ID} id already exists");

            return Ok();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var u = _UsersService.GetById(id);
            if(u == null)
                return NotFound();

            return Ok(u);
        }

        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_UsersService.GetAll());
        }

        // PUT: api/Users/5
        // BODY:
        //  {
        //      "id": 5,
        //      "name": "Abe",
        //      "surname": "Ben",
        //      "eMail": "AbeBen@aggr.io"
        //  }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Put(int id, User user)
        {
            if(id != user.ID)
                return Conflict("IDs are not equal");

            var result = _UsersService.Update(user);
            if (!result)
                return NotFound();

            return Ok();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var result = _UsersService.Delete(id);
            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
