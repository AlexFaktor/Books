using Books.Database.Filters;
using Books.Services.Structures;

namespace Books.Services.Interfaces
{
    /// <summary>
    /// Using this interface, you can make a book source
    /// </summary>
    public interface IBooksImporter
    {
        bool TryGetBook(out Book book);
    }

    /// <summary>
    /// Using this interface, you can make a database requestor class
    /// </summary>
    public interface IDatabaseQueries
    {
        void AddBooks(IBooksImporter books);
        List<Book> FindBooks(BooksFilter filter);
    }
}
