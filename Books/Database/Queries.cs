﻿namespace Books.Database
{
    public class Queries
    {
        public static void AddBook(string[] data)
        {
            using var db = new DatabaseBooksContext();

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

            var pagesSuccessfully = int.TryParse(data[1], out int pages);

            if (!pagesSuccessfully)
            {
                throw new ArgumentException($"The page format is incorrect. Book: {data[0]}");
            }

            Genre genre = db.Genre.FirstOrDefault(g => g.Name == data[2])!;
            if (genre == null)
            {
                genre = new Genre
                {
                    Name = data[2]
                };
                db.Genre.Add(genre);
                db.SaveChanges();
            }

            var dateTimeSuccessfully = DateTime.TryParse(data[3], out DateTime bookDate);

            if (!dateTimeSuccessfully)
            {
                throw new ArgumentException($"The date format is incorrect. Book: {data[0]}");
            }

            Author author = db.Author.FirstOrDefault(a => a.Name == data[4])!;
            if (author == null)
            {
                author = new Author
                {
                    Name = data[4]
                };
                db.Author.Add(author);
                db.SaveChanges();
            }

            Publisher publisher = db.Publisher.FirstOrDefault(p => p.Name == data[5])!;
            if (publisher == null)
            {
                publisher = new Publisher { Name = data[5] };
                db.Publisher.Add(publisher);
                db.SaveChanges();
            }

            Book book = new()
            {
                Title = data[0],
                Pages = pages,
                GenreId = genre.Id,
                ReleaseDate = bookDate,
                AuthorId = author.Id,
                PublisherId = publisher.Id,
            };

            var exists = db.Books.Any(b => b.Title == book.Title 
                                    && b.AuthorId == book.AuthorId 
                                    && b.PublisherId == book.PublisherId);

            if (!exists)
            {
                db.Books.Add(book);
                db.SaveChanges();
            }
        }
    }
}
