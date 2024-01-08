using Newtonsoft.Json;

namespace Books.Database.Entity
{
    public class BooksFilter
    {
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public int? MoreThanPages { get; set; }
        public int? LessThanPages { get; set; }
        public DateTime? PublishedBefore { get; set; }
        public DateTime? PublishedAfter { get; set; }

        /// <summary>
        /// There are no required fields, which makes the filter flexible
        /// </summary>
        public BooksFilter(string? title = default,
                      string? genre = default,
                      string? author = default,
                      string? publisher = default,
                      int? moreThanPages = default,
                      int? lessThanPages = default,
                      DateTime? publishedBefore = default,
                      DateTime? publishedAfter = default)
        {
            Title = title;
            Genre = genre;
            Author = author;
            Publisher = publisher;
            MoreThanPages = moreThanPages;
            LessThanPages = lessThanPages;
            PublishedBefore = publishedBefore;
            PublishedAfter = publishedAfter;
        }
    }
}
