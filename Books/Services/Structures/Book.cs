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
    }
}
