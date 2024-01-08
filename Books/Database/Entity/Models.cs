using Books.Database.Structures;
using System.ComponentModel.DataAnnotations;

namespace Books.Database.Entity
{
    public class Book
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Title { get; set; }
        [Required]
        public int Pages { get; set; }
        [Required]
        public Guid GenreId { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public Guid PublisherId { get; set; }
        public DateTime ReleaseDate { get; set; }

        Book()
        {

        }
        public Book(BookStruct book)
        {
            Title = book.Title;
            Pages = book.Pages;
            GenreId = Genre.GetGuidByName(book.Genre);
            AuthorId = Author.GetGuidByName(book.Author);
            PublisherId = Publisher.GetGuidByName(book.Publisher);
            ReleaseDate = book.ReleaseDate;
        }
    }


    public class Genre
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        public static Guid GetGuidByName(string name)
        {
            var db = new BooksContext();

            var genre = db.Genre.Where(g => g.Name == name)
                .FirstOrDefault()!;
            if (genre == null)
            {

                genre = new Genre { Name = name };
                db.Genre.Add(genre);
                db.SaveChanges();
                return db.Genre.Where(g => g.Name == name).FirstOrDefault()!.Id;
            }

            return genre.Id;
        }
    }

    public class Author
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        public static Guid GetGuidByName(string name)
        {
            var db = new BooksContext();

            var author = db.Author.Where(g => g.Name == name)
                .FirstOrDefault()!;
            if (author == null)
            {

                author = new Author { Name = name };
                db.Author.Add(author);
                db.SaveChanges();
                return db.Author.Where(g => g.Name == name).FirstOrDefault()!.Id;
            }

            return author.Id;
        }
    }

    public class Publisher
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        public static Guid GetGuidByName(string name)
        {
            var db = new BooksContext();

            var publisher = db.Publisher.Where(g => g.Name == name)
                .FirstOrDefault()!;
            if (publisher == null)
            {

                publisher = new Publisher { Name = name };
                db.Publisher.Add(publisher);
                db.SaveChanges();
                return db.Publisher.Where(g => g.Name == name).FirstOrDefault()!.Id;
            }

            return publisher.Id;
        }
    }
}
