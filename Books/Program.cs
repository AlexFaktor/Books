using Books.Database;
using Newtonsoft.Json;

namespace Books
{
    public class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 1)
            {
                try
                {
                    using var db = new DatabaseBooksContext();
                    using StreamReader reader = new(args[0]);

                    string line;
                    int counter = 0;

                    while ((line = reader.ReadLine()!) != null)
                    {
                        string[] data = line.Split(',');
                        try
                        {
                            var book = Queries.ParseBook(data);

                            if (db.Books.Any(b => b.Id == book.Id))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"{data[0]} by {data[4]}, already in the database");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                db.Books.Add(book);
                                db.SaveChanges();
                                counter++;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"{data[0]} by {data[4]}, added to the database successfully");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    Console.WriteLine($"Total books added: {counter}");

                    return 0;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            if (args.Length == 2)
            {
                string jsonContent = File.ReadAllText(args[0]);

                Filter filter = JsonConvert.DeserializeObject<Filter>(jsonContent)!;

                var books = Queries.GetBook(filter);

                using var writer = File.CreateText(args[1]);
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

                return 0;
            }

            return 0;
        }
    }
}
