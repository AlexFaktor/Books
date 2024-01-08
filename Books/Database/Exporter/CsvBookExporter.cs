using Books.Database.Entity;
using Books.Database.Interfaces;
using Newtonsoft.Json;
using System.IO;


namespace Books.Database.Exporter
{
    /// <summary>
    /// Class for exporting books in csv format
    /// </summary>
    public class CsvBookExporter
    {
        /// <summary>
        /// Writes data to a file using a filter and IDatabaseQueries acts as a data source 
        /// </summary>
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
