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
                Queries.AddBooks(FileBookTools.GetDataFromFile(args[0]));
                return 0;
            }
            if (args.Length == 2)
            {
                Queries.GetBooksToFileWithFilter(args[0], args[1]);
                return 0;
            }

            return 0;
        }
    }
}
