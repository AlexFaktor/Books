using Books.Database.Interfaces;
using Books.Database.Structures;

namespace Books.Database.Importer
{
    public class CsvBookImporter : IBooksImporter
    {
        private readonly StreamReader? reader;

        public CsvBookImporter(string path)
        {
            reader = new StreamReader(path);
        }

        private static BookStruct ParseBook(string[] data)
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

            var book = new BookStruct
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

        public bool TryGetBook(out BookStruct book)
        {
            string? line;
            if ((line = reader!.ReadLine()) == null)
            {
                book = default;
                return false;
            }


            book = ParseBook(line.Split(','));
            return true;
        }
    }
}
