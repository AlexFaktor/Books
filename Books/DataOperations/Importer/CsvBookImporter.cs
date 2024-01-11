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

            book = new Book(line.Split(','));
            return true;
        }
    }
}
