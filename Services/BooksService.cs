using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab03.Models;

namespace lab03
{
    public class BooksService : IBooksService
    {
        // NOTE : Hardcoded list
        private readonly List<Book> books = new List<Book>
        {
            new Book{ ID = 0, Title = "Javascript rollercoaster", Author = "JonDoe", IsRented = false },
            new Book{ ID = 1, Title = "Cornercases of JS", Author = "AbeKai", IsRented = false },
            new Book{ ID = 2, Title = "Farming simulator 2k30", Author = "KreTex", IsRented = false },
            new Book{ ID = 3, Title = "How to send http's request", Author = "MeeMee", IsRented = false },
            new Book{ ID = 4, Title = "My glasses are broken", Author = "FovTou", IsRented = false },
            new Book{ ID = 5, Title = "You shall not pass", Author = "GrayWorm", IsRented = false },
            new Book{ ID = 6, Title = "Soft introdction to complex math", Author = "GanTha", IsRented = false },
        };

        public Dictionary<Book, User> rentedBooks = new Dictionary<Book, User>();
        public List<Tuple<Book, User>> rentedBooksHistory = new List<Tuple<Book, User>>();

        public bool AddBook(Book book)
        {
            if (GetById(book.ID) != null)
                return false;

            books.Add(book);
            return true;
        }

        public bool Delete(int id)
        {
            if (GetById(id) == null)
                return false;

            books.RemoveAll(u => u.ID == id);
            return true;
        }

        public List<Book> GetAll()
        {
            return books;
        }

        public Book? GetById(int id)
        {
            return books.FirstOrDefault(u => u.ID == id);
        }

        public bool Update(Book book)
        {
            var b = GetById(book.ID);
            if (b == null)
                return false;

            b.ID = book.ID;
            b.Title = book.Title;
            b.Author = book.Author;
            b.IsRented = book.IsRented;
            return true;
        }

        public Dictionary<Book, User> GetRentedBooks()
        {
            return rentedBooks;
        }
        public List<Tuple<Book, User>> GetRentedBooksHistory()
        {
            return rentedBooksHistory;
        }
    }
}
