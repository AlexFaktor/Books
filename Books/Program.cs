namespace Books
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DatabaseBooksContext())
            {
                var books = db.Books.ToList();

                foreach (var book in books)
                {
                    db.Books.Remove(book);
                }

                db.SaveChanges();
            }
        }
    }
}
