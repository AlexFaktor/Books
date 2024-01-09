using Books.Database.Queries;
using Books.Database.Importer;

namespace Books.Test
{
    [TestClass]
    public class BooksTests
    {
        readonly BooksContextTest db;
        readonly DatabaseQueries dbQueries;

        BooksTests()
        {
            db = new BooksContextTest();
            dbQueries = new DatabaseQueries(db);
        }
        
        [TestMethod]
        public void AddBooks_WithValidInput_ShouldReturnExpectedValue()
        {
            var numberOfBooks = 41;
            var numberOfGenres = 16;
            var numberOfAuthor = 36;
            var numberOfPublishers = 34;

            dbQueries.AddBooks(new CsvBookImporter("books.csv"));

            Assert.AreEqual(numberOfBooks, db.Books.Count());
            Assert.AreEqual(numberOfGenres, db.Genre.Count());
            Assert.AreEqual(numberOfAuthor, db.Author.Count());
            Assert.AreEqual(numberOfPublishers, db.Publisher.Count());
        }

        /*
        [TestMethod]
        [DataRow()]
        public void GetFilteredBooks_WithValidInput_ShouldReturnExpectedValue()
        {

        }
        */
    }
}