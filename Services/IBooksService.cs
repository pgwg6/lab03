using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab03.Models;

namespace lab03
{
    public interface IBooksService
    {
        public bool AddBook(Book user);
        public Book? GetById(int id);
        public List<Book> GetAll();
        public bool Update(Book user);
        public bool Delete(int id);

        public Dictionary<Book, User> GetRentedBooks();
        public List<Tuple<Book, User>> GetRentedBooksHistory();
    }
}
