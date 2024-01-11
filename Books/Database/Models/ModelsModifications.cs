using Books.Services.Structures;
using System.Linq;

namespace Books.Database.Models
{
    public partial class RecordBook
    {
        public RecordBook()
        {

        }
        public RecordBook(Book book, BooksContext db)
        {
            Title = book.Title;
            Pages = book.Pages;
            GenreId = Genre.GetGuidByName(book.Genre, db);
            AuthorId = Author.GetGuidByName(book.Author, db);
            PublisherId = Publisher.GetGuidByName(book.Publisher, db);
            ReleaseDate = book.ReleaseDate;
        }
    }
    
    public partial class Genre
    {
        /// <summary>
        /// Since the Guid is not attached to the data, you need to check for duplication.
        /// If the database has such data, the return function will return it, else will generate a new Guid 
        /// </summary>
        public static Guid GetGuidByName(string name, BooksContext db)
        {
            var genre = db.Genre.Where(g => g.Name == name)
                .FirstOrDefault();
            if (genre == null)
            {

                genre = new Genre { Name = name };
                db.Genre.Add(genre);
                db.SaveChanges();
                return db.Genre.FirstOrDefault(g => g.Name == name)!.Id;
            }

            return genre.Id;
        }
    }

    public partial class Author
    {
        /// <summary>
        /// Since the Guid is not attached to the data, you need to check for duplication.
        /// If the database has such data, the return function will return it, else will generate a new Guid 
        /// </summary>
        public static Guid GetGuidByName(string name, BooksContext db)
        {
            var author = db.Author.Where(g => g.Name == name)
                .FirstOrDefault();
            if (author == null)
            {

                author = new Author { Name = name };
                db.Author.Add(author);
                db.SaveChanges();
                return db.Author.FirstOrDefault(g => g.Name == name)!.Id;
            }

            return author.Id;
        }
    }

    public partial class Publisher
    {
        /// <summary>
        /// Since the Guid is not attached to the data, you need to check for duplication.
        /// If the database has such data, the return function will return it, else will generate a new Guid 
        /// </summary>
        public static Guid GetGuidByName(string name, BooksContext db)
        {
            var publisher = db.Publisher.Where(g => g.Name == name)
                .FirstOrDefault();
            if (publisher == null)
            {

                publisher = new Publisher { Name = name };
                db.Publisher.Add(publisher);
                db.SaveChanges();
                return db.Publisher.FirstOrDefault(g => g.Name == name)!.Id;
            }

            return publisher.Id;
        }
    }
}
