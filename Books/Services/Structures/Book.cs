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

        public Book(string[] data)
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

            Title = data[0];
            Pages = bookPages;
            Genre = data[2];
            ReleaseDate = bookDate;
            Author = data[4];
            Publisher = data[5];
        }
    }
}
