using Books.Services.Structures;

namespace Books.Database.Models
{
    public static class BookExtensions
    {
        public static RecordBook ToRecordBook(this Book book, BooksContext db)
        {
            RecordBook recordBook = new()
            {
                Title = book.Title,
                Pages = book.Pages,
                GenreId = GenreExtensions.GetGuidByName(book.Genre, db),
                AuthorId = AuthorExtensions.GetGuidByName(book.Author, db),
                PublisherId = PublisherExtensions.GetGuidByName(book.Publisher, db),
                ReleaseDate = book.ReleaseDate
            };

            return recordBook;
        }
    }

    public static class GenreExtensions
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
                genre = new Genre { Name = name, Id = Guid.NewGuid() };
                db.Genre.Add(genre);
                db.SaveChanges();
            }

            return genre.Id;
        }
    }

    public static class AuthorExtensions
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
                author = new Author { Name = name, Id = Guid.NewGuid() };
                db.Author.Add(author);
                db.SaveChanges();
            }

            return author.Id;
        }
    }

    public static class PublisherExtensions
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
                publisher = new Publisher { Name = name, Id = Guid.NewGuid() };
                db.Publisher.Add(publisher);
                db.SaveChanges();
            }

            return publisher.Id;
        }
    }
}
