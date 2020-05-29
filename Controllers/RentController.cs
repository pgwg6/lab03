using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lab03.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace lab03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RentController : ControllerBase
    {
        private IUsersService _UsersService;
        private IBooksService _BooksService;

        public RentController(IUsersService usersService, IBooksService booksService)
        {
            _UsersService = usersService;
            _BooksService = booksService;
        }

        // POST: api/Rent
        // BODY:
        //  {
        //      "bookid": 3
        //  }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddRent(Rent rent)
        {
            var user = _UsersService.AddUser(this.User);
            var book = _BooksService.GetById(rent.BookId);

            if (user == null || book == null)
                return NotFound();

            //if (book.IsRented)
            //    return Conflict("Book has been already rented");

            book.IsRented = true;

            if(_BooksService.GetRentedBooks().ContainsKey(book))
            {
                _BooksService.GetRentedBooks()[book] = user;
            }
            else
            {
                _BooksService.GetRentedBooks().Add(book, user);
            }

            _BooksService.GetRentedBooksHistory().Add(new Tuple<Book, User>(book, user));
            return Ok();
        }

        // GET: api/Rent/User/5
        [Route("User/{id}")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByUserId(int id)
        {
            var user = _UsersService.GetById(id);
            if (user == null)
                return NotFound("User not found");

            var books = _BooksService.GetRentedBooks()
                .Where(r => r.Value.ID == id)
                .Select(r => r.Key)
                .ToArray();

            return Ok(books);
        }

        // GET: api/Rent/User
        [Route("User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByUserId()
        {
            var user = _UsersService.AddUser(this.User);
            if (user == null)
                return NotFound("User not found");

            var books = _BooksService.GetRentedBooks()
                .Where(r => r.Value.ID == user.ID)
                .Select(r => r.Key)
                .ToArray();

            return Ok(books);
        }


        // GET: api/Rent/Book/5
        [Route("Book/{id}")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByBookId(int id)
        {
            var b = _BooksService.GetById(id);
            if (b == null)
                return NotFound("Book not found");

            var usersInHistory = _BooksService.GetRentedBooksHistory()
                .Where(r => r.Item1.ID == id)
                .Select(r => r.Item2)
                .Distinct()
                .ToArray();

            return Ok(usersInHistory);
        }
    }
}