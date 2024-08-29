namespace Books.Database.Filters
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
        /// for JSON
        /// </summary>
        public BooksFilter()
        {

        }

        /// <summary>
        /// for tests
        /// </summary>
        public BooksFilter(string? title = default,
                      string? genre = default,
                      string? author = default,
                      string? publisher = default,
                      int? moreThanPages = default,
                      int? lessThanPages = default,
                      string? publishedBefore = default,
                      string? publishedAfter = default)
        {
            Title = title;
            Genre = genre;
            Author = author;
            Publisher = publisher;
            MoreThanPages = moreThanPages;
            LessThanPages = lessThanPages;

            PublishedBefore = publishedBefore != null ? DateTime.Parse(publishedBefore) : null;
            PublishedAfter = publishedAfter != null ? DateTime.Parse(publishedAfter) : null;
        }
    }
}
