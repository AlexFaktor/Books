using Books.Database;
using Books.Database.Entity;
using Books.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Books.Test
{
    public class BooksContextTest : BooksContext
    { 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "DatabaseBooksTest");
        }
    }
}
