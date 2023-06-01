using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Books_DB_Lemetti
{
    class BookContext : DbContext
    {
        public BookContext() : base("BooksConnect")
        { }

        public DbSet<Book> Books { get; set; }
    }
}
