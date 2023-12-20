using Books.Database;

namespace Books
{
    public class Program
    {    
        static int Main(string[] args)
        {
            if (args.Length == 1)
            {
                using StreamReader reader = new(args[0]);

                string line;
                int counter = 0;

                while ((line = reader.ReadLine()!) != null)
                {
                    string[] data = line.Split(',');
                    try
                    {
                        Queries.AddBook(data);
                        counter++;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{data[0]} by {data[4]} added to the database successfully");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    catch(Exception ex) 
                    { 
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.WriteLine($"Total books added: {counter}");

                return 0;
            }
            if (args.Length == 0)
            {
                var books = Queries.GetBook(new Filter(title: "19"));

                foreach (var book in books)
                {
                    Console.WriteLine(book.Title);
                }

                return 0;
            }
            
            return 0;
        }
    }
}
