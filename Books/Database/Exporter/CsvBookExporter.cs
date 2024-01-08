﻿using Books.Database.Entity;
using Books.Database.Interfaces;
using Newtonsoft.Json;

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
            try
            {
                BooksFilter filter = JsonConvert.DeserializeObject<BooksFilter>(File.ReadAllText(pathFilter))! ?? throw new JsonException("Incorrect data in the filter");

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
                Console.WriteLine($"In \"{pathTo}\" was written {books.Count} books");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
