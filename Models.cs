using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab03
{
    namespace Models
    {
        public class User
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string EMail { get; set; }
        }

        public class Book
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public bool IsRented { get; set; }
        }


        public class Rent
        {
            public int UserId { get; set; }
            public int BookId { get; set; }
        }
    }
}
