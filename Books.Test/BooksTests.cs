namespace Books.Test
{
    [TestClass]
    public class BooksTests
    {
        readonly BooksContextTest db = new();
        

        [TestMethod]
        public void AddBooks_WithValidInput_ShouldReturnExpectedValue()
        {
            // var numberOfBooks = 41;
            // var numberOfGenres = 16;
            // var numberOfAuthor = 36;
            // var numberOfPublishers = 34;


        }

        [TestMethod]
        [DataRow()]
        public void GetFilteredBooks_WithValidInput_ShouldReturnExpectedValue()
        {

        }
    }
}