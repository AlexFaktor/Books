﻿using Books.Database.Models;
using Books.Database.Queries;
using Books.DataOperations.Exporter;
using Books.DataOperations.Importer;

namespace Books
{
    public class Program
    {
        static int Main(string[] args)
        {
            var db = new DatabaseQueries(new BooksContext());

            if (args.Length == 1)
            {
                db.AddBooks(new CsvBookImporter(args[0]));
                return 0;
            }
            if (args.Length == 2)
            {
                CsvBookExporter.WriteBooksToFile(args[0], args[1], db);
                return 0;
            }

            return 0;
        }
    }
}
