using Newtonsoft.Json;

namespace Books.Database
{
    public class Filter
    {
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public int? MoreThanPages { get; set; }
        public int? LessThanPages { get; set; }
        public DateTime? PublishedBefore { get; set; }
        public DateTime? PublishedAfter { get; set; }

        public Filter(string? title = default, 
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

        public Filter(string path )
        {
            string json = File.ReadAllText(path);
            Filter filterFromJSON = JsonConvert.DeserializeObject<Filter>(json)!;
        Title = filterFromJSON.Title;
        Genre = filterFromJSON.Genre;
        Author = filterFromJSON.Author;
        Publisher = filterFromJSON.Publisher;
        MoreThanPages = filterFromJSON.MoreThanPages;
        LessThanPages = filterFromJSON.LessThanPages;
        PublishedBefore = filterFromJSON.PublishedBefore;
        PublishedAfter = filterFromJSON.PublishedAfter;
        }
    }
}
