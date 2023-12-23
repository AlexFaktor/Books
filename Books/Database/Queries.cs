using Newtonsoft.Json;

namespace Books.Database
{
    public class FileBookTools
    {
        public static List<string[]> GetDataFromFile(string path)
        {
            using StreamReader reader = new(path);

            List<string[]> result = new();
            string line;
            while ((line = reader.ReadLine()!) != null)
            {
                string[] data = line.Split(',');
                result.Add(data);
            }
            return result;
        }
    }

    public class Queries
    {
        public static void AddBook(string[] data)
        {
            using var db = new DatabaseBooksContext();

            var book = new Book(data);
            var genre = new Genre(data[2]);
            var author = new Author(data[4]);
            var publisher = new Publisher(data[5]);

            if (!db.Genre.Any(g => g == genre))
                db.Genre.Add(genre);
            if (!db.Author.Any(a => a == author))
                db.Author.Add(author);
            if (!db.Publisher.Any(p => p == publisher))
                db.Publisher.Add(publisher);
            if (!db.Books.Any(b => b == book))
            {
                db.Books.Add(book);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{data[0]} by {data[4]}, added to the database successfully");
                Console.ForegroundColor = ConsoleColor.White;
                db.SaveChanges();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{data[0]} by {data[4]}, already in the database");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static List<Book> GetFilteredBooks(Filter filter)
        {
            using var db = new DatabaseBooksContext();

            var possibleGenres = db.Genre.Where(g => g.Name!.Contains(filter.Genre!))
                .ToList()
                .Select(g => g.Id)
                .ToList();
            var possibleAuthor = db.Author.Where(a => a.Name!.Contains(filter.Author!))
                .ToList()
                .Select(a => a.Id)
                .ToList();
            var possiblePublisher = db.Publisher.Where(p => p.Name!.Contains(filter.Publisher!))
                .ToList()
                .Select(p => p.Id)
                .ToList();

            var books = db.Books.Where(b =>
                                    (filter.Title == null || b.Title!.Contains(filter.Title)) &&
                                    (filter.Genre == null || possibleGenres.Contains(b.GenreId!)) &&
                                    (filter.Author == null || possibleAuthor.Contains(b.AuthorId!)) &&
                                    (filter.Publisher == null || possiblePublisher.Contains(b.PublisherId!)) &&
                                    (filter.MoreThanPages == null || b.Pages >= filter.MoreThanPages) &&
                                    (filter.LessThanPages == null || b.Pages < filter.LessThanPages) &&
                                    (filter.PublishedBefore == null || b.ReleaseDate < filter.PublishedBefore) &&
                                    (filter.PublishedAfter == null || b.ReleaseDate > filter.PublishedAfter)
                                    );

            return books.ToList();
        }

        public static void AddBooks(List<string[]> books)
        {
            try
            {
                using var db = new DatabaseBooksContext();

                int counter = 0;

                while (books.Count != 0)
                {
                    try
                    {
                        var firstElement = books.First();
                        books.Remove(firstElement);
                        AddBook(firstElement);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                Console.WriteLine($"Total books added: {counter}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void GetBooksToFileWithFilter(string pathToFilter, string pathToFile)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(File.ReadAllText(pathToFilter))!;

            var books = GetFilteredBooks(filter);

            using var writer = File.CreateText(pathToFile);
            using var db = new DatabaseBooksContext();

            foreach (var book in books)
            {
                var result = $"{book.Title}," +
                             $"{book.Pages}," +
                             $"{db.Genre.Where(g => g.Id == book.GenreId).ToArray()[0].Name}," +
                             $"{db.Author.Where(a => a.Id == book.AuthorId).ToArray()[0].Name}," +
                             $"{db.Publisher.Where(p => p.Id == book.PublisherId).ToArray()[0].Name}," +
                             $"{book.ReleaseDate:yyyy-MM-dd}";

                writer.WriteLine(result);
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Books available on request: {books.Count}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
