using Books.Services.Structures;
using Books.Services.Tools;

namespace Books.Database.Models
{
    public static class BookExtensions
    {
        public static RecordBook ToRecordBook(this Book book, BooksContext db)
        {
            RecordBook recordBook = new()
            {
                Title = book.Title,
                Pages = book.Pages,
                GenreId = GenreTools.GetGuidByName(book.Genre, db),
                AuthorId = AuthorTools.GetGuidByName(book.Author, db),
                PublisherId = PublisherTools.GetGuidByName(book.Publisher, db),
                ReleaseDate = book.ReleaseDate
            };

            return recordBook;
        }

        public static Book ToBook(this RecordBook recordBook, BooksContext db)
        {
            Book book = new()
            {
                Title = recordBook.Title!,
                Pages = recordBook.Pages,
                Genre = db.Genre.FirstOrDefault(g => g.Id == recordBook.GenreId)!.Name!,
                Author = db.Author.FirstOrDefault(g => g.Id == recordBook.AuthorId)!.Name!,
                Publisher = db.Publisher.FirstOrDefault(g => g.Id == recordBook.PublisherId)!.Name!,
                ReleaseDate = recordBook.ReleaseDate
            };

            return book;
        }
    }
}
