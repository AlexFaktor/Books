using System.ComponentModel.DataAnnotations;

namespace Books.Database.Models
{
    public class RecordBook
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
    }


    public partial class Genre
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }
    }

    public partial class Author
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }
    }

    public partial class Publisher
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }
    }
}
