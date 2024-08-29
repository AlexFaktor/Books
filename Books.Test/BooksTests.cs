using Books.Database.Filters;
using Books.Database.Queries;
using Books.DataOperations.Importer;

namespace Books.Test
{
    [TestClass]
    [DeploymentItem("books.csv")]
    public class BooksTests
    {
        private BooksContextTest? db;
        private DatabaseQueries? dbQueries;

        [TestInitialize]
        public void TestInitialize()
        {
            db = new BooksContextTest();
            db.Database.EnsureDeleted(); 
            db.Database.EnsureCreated(); 
            dbQueries = new DatabaseQueries(db);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            db!.Dispose();
        }

        [TestMethod]
        public void AddBooks_WithValidInput_ShouldReturnExpectedValue()
        {
            var numberOfBooks = 41;
            var numberOfGenres = 16;
            var numberOfAuthors = 36;
            var numberOfPublishers = 34;

            dbQueries!.AddBooks(new CsvBookImporter("books.csv"));

            Assert.AreEqual(numberOfBooks, db!.Books.Count());
            Assert.AreEqual(numberOfGenres, db.Genre.Count());
            Assert.AreEqual(numberOfAuthors, db.Author.Count());
            Assert.AreEqual(numberOfPublishers, db.Publisher.Count());
        }

        [TestMethod]
        [DataRow(null, null, null, null, null, null, null, null, 41)]
        [DataRow("thE", null, null, null, null, null, null, null, 22)]
        [DataRow(null, "Hor", null, null, null, null, null, null, 3)]
        [DataRow(null, null, "john", null, null, null, null, null, 1)]
        [DataRow(null, null, null, "the", null, null, null, null, 5)]
        [DataRow(null, null, null, null, 200, null, null, null, 34)]
        [DataRow(null, null, null, null, null, 200, null, null, 7)]
        [DataRow(null, null, null, null, null, null, "1900-01-01", null, 15)]
        [DataRow(null, null, null, null, null, null, null, "1900-01-01", 26)]
        [DataRow("t", "o", "a", "a", 30, 1000, "3000-01-01", "0001-01-01", 8)]
        public void GetFilteredBooks_WithValidInput_ShouldReturnExpectedValue
            (string? title, string? genre, string? author, string? publisher,
            int? moreThanPages, int? lessThanPages, string? publishedBefore, string? publishedAfter,
            int amount)
        {
            var filter = new BooksFilter(title: title, genre: genre, author: author, publisher: publisher,
                moreThanPages: moreThanPages, lessThanPages: lessThanPages, publishedBefore: publishedBefore, publishedAfter: publishedAfter);

            dbQueries!.AddBooks(new CsvBookImporter("books.csv"));
            var result = dbQueries.FindBooks(filter).Count;
            
            Assert.AreEqual(result, amount);
        }
    }
}