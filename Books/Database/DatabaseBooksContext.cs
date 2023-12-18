using Microsoft.EntityFrameworkCore;

namespace Books.Database
{
    public class DatabaseBooksContext : DbContext
    {
        public DatabaseBooksContext()
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
