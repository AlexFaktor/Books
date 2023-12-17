namespace Books
{
    public class Book
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public int Pages { get; set; }
        public Guid GenreId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PublisherId { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    public class Genre
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    public class Author
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    public class Publisher
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
