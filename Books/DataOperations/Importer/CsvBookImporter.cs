using Books.Services.Interfaces;
using Books.Services.Structures;

namespace Books.DataOperations.Importer
{
    /// <summary>
    /// Class for importing books in csv format
    /// </summary>
    public class CsvBookImporter : IBooksImporter
    {
        private readonly StreamReader? reader;

        public CsvBookImporter(string path)
        {
            reader = new StreamReader(path);
        }

        private static Book CreateBookFromArray(string[] data)
        {
            if (data[0].Length > 255)
                throw new ArgumentException($"The book name is too long. Book: {data[0]}");
            if (!int.TryParse(data[1], out int bookPages))
                throw new ArgumentException($"The page format is incorrect. Book: {data[0]}");
            if (data[2].Length > 255)
                throw new ArgumentException($"The genre of the book is too long. Book: {data[0]}");
            if (!DateTime.TryParse(data[3], out DateTime bookDate))
                throw new ArgumentException($"The date format is incorrect. Book: {data[0]}");
            if (data[4].Length > 255)
                throw new ArgumentException($"The name of the author of the book is too long. Book: {data[0]}");
            if (data[5].Length > 255)
                throw new ArgumentException($"The publisher's name is too long. Book: {data[0]}");

            Book book = new()
            {
                Title = data[0],
                Pages = bookPages,
                Genre = data[2],
                ReleaseDate = bookDate,
                Author = data[4],
                Publisher = data[5]
            };
            
            return book;
        }

        /// <summary>
        /// Returns true if there are books to return
        /// </summary>
        public bool TryGetBook(out Book book)
        {
            string? line;
            if ((line = reader!.ReadLine()) == null)
            {
                book = default;
                return false;
            }

            book = CreateBookFromArray(line.Split(','));
            return true;
        }
    }
}
