using Books.Database.Models;
using System.ComponentModel.DataAnnotations;

namespace Books.Services.Structures
{
    /// <summary>
    /// Book structure without Guid
    /// </summary>
    public struct Book
    {
        [MaxLength(255)]
        public string Title { get; set; }
        public int Pages { get; set; }
        [MaxLength(255)]
        public string Genre { get; set; }
        [MaxLength(255)]
        public string Author { get; set; }
        [MaxLength(255)]
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }

        public Book(RecordBook book, BooksContext db)
        {
            Title = book.Title!;
            Pages = book.Pages;
            Genre = db.Genre.Where(g => g.Id == book.GenreId).FirstOrDefault()!.Name!;
            Author = db.Author.Where(g => g.Id == book.AuthorId).FirstOrDefault()!.Name!;
            Publisher = db.Publisher.Where(g => g.Id == book.PublisherId).FirstOrDefault()!.Name!;
            ReleaseDate = book.ReleaseDate;
        }
    }
}
