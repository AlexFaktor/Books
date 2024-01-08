using Books.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace Books.Test
{
    public class DatabaseBooksContextTest : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Publisher> Publisher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "DatabaseBooksTest");
        }
    }
}
