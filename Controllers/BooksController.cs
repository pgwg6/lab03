using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lab03.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace lab03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowOnlyLocally")]
    public class BooksController : ControllerBase
    {
        private IBooksService _BooksService;
        public BooksController(IBooksService booksService)
        {
            _BooksService = booksService;
        }

        // POST: api/Books
        // BODY:
        //  {
        //      "id": 5,
        //      "title": "Abe",
        //      "author": "Ben",
        //      "isRented": false
        //  }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Post(Book book)
        {
            var result = _BooksService.AddBook(book);
            if (!result)
                return Conflict($"Book with {book.ID} id already exists");

            return Ok();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var b = _BooksService.GetById(id);
            if (b == null)
                return NotFound();

            return Ok(b);
        }

        // GET: api/Books
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_BooksService.GetAll());
        }

        // PUT: api/Books/5
        // BODY:
        //  {
        //      "id": 5,
        //      "title": "Abe",
        //      "author": "Ben",
        //      "isRented": false
        //  }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Put(int id, Book book)
        {
            if (id != book.ID)
                return Conflict("IDs are not equal");

            var result = _BooksService.Update(book);
            if (!result)
                return NotFound();

            return Ok();
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var result = _BooksService.Delete(id);
            if (!result)
                return NotFound();

            return Ok();
        }
    }
}