using Books.Database.Entity;
using Books.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Books.Database
{
    public class BooksContext : DbContext
    {
        public BooksContext()
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Publisher> Publisher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Books;Integrated Security=True;");
        }
    }
}
