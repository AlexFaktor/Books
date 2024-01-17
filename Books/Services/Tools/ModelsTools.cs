using Books.Database.Models;

namespace Books.Services.Tools
{
    public static class GenreTools
    {
        /// <summary>
        /// Since the Guid is not attached to the data, you need to check for duplication.
        /// If the database has such data, the return function will return it, else will generate a new Guid 
        /// </summary>
        public static Guid GetGuidByName(string name, BooksContext db)
        {
            var genre = db.Genre.FirstOrDefault(g => g.Name == name);
            if (genre == null)
            {
                genre = new Genre { Name = name, Id = Guid.NewGuid() };
                db.Genre.Add(genre);
                db.SaveChanges();
            }

            return genre.Id;
        }
    }

    public static class AuthorTools
    {
        /// <summary>
        /// Since the Guid is not attached to the data, you need to check for duplication.
        /// If the database has such data, the return function will return it, else will generate a new Guid 
        /// </summary>
        public static Guid GetGuidByName(string name, BooksContext db)
        {
            var author = db.Author.FirstOrDefault(g => g.Name == name);
            if (author == null)
            {
                author = new Author { Name = name, Id = Guid.NewGuid() };
                db.Author.Add(author);
                db.SaveChanges();
            }

            return author.Id;
        }
    }

    public static class PublisherTools
    {
        /// <summary>
        /// Since the Guid is not attached to the data, you need to check for duplication.
        /// If the database has such data, the return function will return it, else will generate a new Guid 
        /// </summary>
        public static Guid GetGuidByName(string name, BooksContext db)
        {
            var publisher = db.Publisher.FirstOrDefault(g => g.Name == name);
            if (publisher == null)
            {
                publisher = new Publisher { Name = name, Id = Guid.NewGuid() };
                db.Publisher.Add(publisher);
                db.SaveChanges();
            }

            return publisher.Id;
        }
    }

}
