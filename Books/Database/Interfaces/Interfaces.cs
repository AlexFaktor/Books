using Books.Database.Entity;
using Books.Database.Structures;

namespace Books.Database.Interfaces
{
    /// <summary>
    /// Using this interface, you can make a book source
    /// </summary>
    public interface IBooksImporter
    {
        bool TryGetBook(out BookStruct book);
    }

    /// <summary>
    /// Using this interface, you can make a database requestor class
    /// </summary>
    public interface IDatabaseQueries
    {
        void AddBooks(IBooksImporter books);
        List<BookStruct> FindBooks(BooksFilter filter);
    }
}
