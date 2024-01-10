using Books.Database.Filters;
using Books.Database.Models;
using Books.Services.Interfaces;
using Books.Services.Structures;

namespace Books.Database.Queries
{
    /// <summary>
    /// Class of database queries
    /// </summary>
    public class DatabaseQueries : IDatabaseQueries
    {
        private readonly BooksContext db;

        public DatabaseQueries(BooksContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Adds books from the source
        /// </summary>
        public void AddBooks(IBooksImporter books)
        {
            bool import = true;

            do
            {
                try
                {
                    import = books.TryGetBook(out Book book);
                    if (import)
                    {
                        var bookDb = new RecordBook(book, db);
                        var isRepeatedBooks = db.Books.Where(b => b.Title == bookDb.Title &&
                                            b.Pages == bookDb.Pages &&
                                            b.GenreId == bookDb.GenreId &&
                                            b.AuthorId == bookDb.AuthorId &&
                                            b.PublisherId == bookDb.PublisherId &&
                                            b.ReleaseDate == bookDb.ReleaseDate)
                                            .ToArray()
                                            .Length == 0;
                        if (isRepeatedBooks)
                        {
                            db.Books.Add(bookDb);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{book.Title} by {book.Author}, added to the database successfully");
                            Console.ResetColor();
                            db.SaveChanges();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{book.Title} by {book.Author}, already in the database");
                            Console.ResetColor();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
            while (import);
        }

        /// <summary>
        /// Filters books and returns them in a convenient format
        /// </summary>
        public List<Book> FindBooks(BooksFilter filter)
        {
            var possibleGenres = db.Genre.Where(g => g.Name!.Contains(filter.Genre!)).Select(g => g.Id);
            var possibleAuthor = db.Author.Where(g => g.Name!.Contains(filter.Author!)).Select(g => g.Id);
            var possiblePublisher = db.Publisher.Where(g => g.Name!.Contains(filter.Publisher!)).Select(g => g.Id);

            var books = db.Books.Where(b =>
                                    (filter.Title == null || b.Title!.Contains(filter.Title)) &&
                                    (filter.Genre == null || possibleGenres.Contains(b.GenreId)) &&
                                    (filter.Author == null || possibleAuthor.Contains(b.AuthorId)) &&
                                    (filter.Publisher == null || possiblePublisher.Contains(b.PublisherId)) &&
                                    (filter.MoreThanPages == null || b.Pages >= filter.MoreThanPages) &&
                                    (filter.LessThanPages == null || b.Pages < filter.LessThanPages) &&
                                    (filter.PublishedBefore == null || b.ReleaseDate < filter.PublishedBefore) &&
                                    (filter.PublishedAfter == null || b.ReleaseDate > filter.PublishedAfter)
                                    ).ToList();

            var result = new List<Book>();
            foreach ( var book in books )
            {
                result.Add(new Book(book, db));
            }

            return result;
        }
    }
}
