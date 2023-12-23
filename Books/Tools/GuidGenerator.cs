using Books.Database;
using System.Security.Cryptography;
using System.Text;

namespace Books.Tools
{
    public class GuidGenerator
    {
        public static byte[] GetBytesFrom(string line)
        {
            return Encoding.UTF8.GetBytes($"{line}");
        }
        public static byte[] GetBytesFrom(Genre genre)
        {
            return Encoding.UTF8.GetBytes($"{genre.Name}");
        }
        public static byte[] GetBytesFrom(Author author)
        {
            return Encoding.UTF8.GetBytes($"{author.Name}");
        }
        public static byte[] GetBytesFrom(Publisher publisher)
        {
            return Encoding.UTF8.GetBytes($"{publisher.Name}");
        }
        public static byte[] GetBytesFrom(Book book)
        {
            return Encoding.UTF8.GetBytes($"{book.Title}{book.Pages}{book.GenreId}{book.AuthorId}{book.ReleaseDate}{book.PublisherId}");
        }

        public static Guid GenerateGuidFromData(byte[] dataBytes)
        {
            byte[] hashBytes = SHA256.HashData(dataBytes);

            byte[] guidBytes = new byte[16];
            Array.Copy(hashBytes, guidBytes, 16);

            return new Guid(guidBytes);
        }
    }
}
