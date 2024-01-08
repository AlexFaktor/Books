using Books.Database.Entity;
using Books.Database.Structures;

namespace Books.Database.Interfaces
{
    public interface IBooksImporter
    {
        bool TryGetBook(out BookStruct book);
    }

    public interface IDatabaseQueries
    {
        void AddBook(IBooksImporter books);
        List<BookStruct> FindBooks(BooksFilter filter);
    }
}
