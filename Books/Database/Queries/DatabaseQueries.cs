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
            IQueryable<RecordBook> query = db.Books;

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                query = query.Where(b => b.Title!.Contains(filter.Title));
            }

            if (filter.Genre is not null)
            {
                var possibleGenres = db.Genre.Where(g => g.Name!.Contains(filter.Genre!)).Select(g => g.Id);
                query = query.Where(b => possibleGenres.Contains(b.GenreId));
            }

            if (filter.Author is not null)
            {
                var possibleAuthor = db.Author.Where(g => g.Name!.Contains(filter.Author!)).Select(g => g.Id);
                query = query.Where(b => possibleAuthor.Contains(b.AuthorId));
            }

            if (filter.Publisher is not null)
            {
                var possiblePublisher = db.Publisher.Where(g => g.Name!.Contains(filter.Publisher!)).Select(g => g.Id);
                query = query.Where(b => possiblePublisher.Contains(b.PublisherId));
            }

            if (filter.MoreThanPages is not null)
            {
                query = query.Where(b => b.Pages > filter.MoreThanPages);
            }

            if (filter.LessThanPages is not null)
            {
                query = query.Where(b => b.Pages < filter.LessThanPages);
            }

            if (filter.PublishedBefore is not null)
            {
                query = query.Where(b => b.ReleaseDate < filter.PublishedBefore);
            }

            if (filter.PublishedAfter is not null)
            {
                query = query.Where(b => b.ReleaseDate > filter.PublishedAfter);
            }

            var books = query.ToList();

            return books.Select(i => new Book(i, db )).ToList();
        }
    }
}