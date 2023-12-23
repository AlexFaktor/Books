using Books.Tools;
using System.ComponentModel.DataAnnotations;

namespace Books.Database
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

        public Book()
        {

        }
        public Book(string[] data)
        {
            if (data[0].Length > 255)
            {
                throw new ArgumentException($"The book name is too long. Book: {data[0]}");
            }
            if (data[2].Length > 255)
            {
                throw new ArgumentException($"The genre of the book is too long. Book: {data[0]}");
            }
            if (data[4].Length > 255)
            {
                throw new ArgumentException($"The name of the author of the book is too long. Book: {data[0]}");
            }
            if (data[5].Length > 255)
            {
                throw new ArgumentException($"The publisher's name is too long. Book: {data[0]}");
            }
            if (!int.TryParse(data[1], out int pages))
            {
                throw new ArgumentException($"The page format is incorrect. Book: {data[0]}");
            }
            if (!DateTime.TryParse(data[3], out DateTime bookDate))
            {
                throw new ArgumentException($"The date format is incorrect. Book: {data[0]}");
            }

            Genre genre = new(data[2]);
            Author author = new(data[4]);
            Publisher publisher = new(data[5]);

            Id = GuidGenerator.GenerateGuidFromData(GuidGenerator.GetBytesFrom($"{data[0]}{pages}{genre.Id}{author.Id}{bookDate}{publisher.Id}"));
            Title = data[0];
            Pages = pages;
            GenreId = genre.Id;
            AuthorId = author.Id;
            ReleaseDate = bookDate;
            PublisherId = publisher.Id;
        }
    }

    public class Genre
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        public Genre(string name)
        {
            if (name.Length > 256)
                throw new ArgumentException("The genre name must be less than 256 characters");

            Id = GuidGenerator.GenerateGuidFromData(GuidGenerator.GetBytesFrom(name));
            Name = name;
        }
    }

    public class Author
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        public Author(string name)
        {
            if (name.Length > 256)
                throw new ArgumentException("The author name must be less than 256 characters");

            Id = GuidGenerator.GenerateGuidFromData(GuidGenerator.GetBytesFrom(name));
            Name = name;
        }
    }

    public class Publisher
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        public Publisher(string name)
        {
            if (name.Length > 256)
                throw new ArgumentException("The publisher name must be less than 256 characters");

            Id = GuidGenerator.GenerateGuidFromData(GuidGenerator.GetBytesFrom(name));
            Name = name;
        }
    }
}
