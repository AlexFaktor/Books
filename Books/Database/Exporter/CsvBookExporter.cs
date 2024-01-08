using Books.Database.Entity;
using Books.Database.Interfaces;
using Newtonsoft.Json;
using System.IO;


namespace Books.Database.Exporter
{
    public class CsvBookExporter
    {
        public static void WriteBooksToFile(string pathTo, string pathFilter, IDatabaseQueries from)
        {
            BooksFilter filter = JsonConvert.DeserializeObject<BooksFilter>(File.ReadAllText(pathFilter))!;

            var books = from.FindBooks(filter);
            using var writer = File.CreateText(pathTo);

            foreach (var book in books)
            {
                var result = $"{book.Title}," +
                             $"{book.Pages}," +
                             $"{book.Genre}," +
                             $"{book.ReleaseDate:yyyy-MM-dd}," +
                             $"{book.Author}," +
                             $"{book.Publisher}";

                writer.WriteLine(result);
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Books available on request: {books.Count}");
            Console.ResetColor();

        }
    }
}
