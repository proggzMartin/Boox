using Boox.Core.Models.Entities;
using Boox.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Boox.Tests
{
    public static class TestTools
    {
        public static void AddTestBooksToContext(BooxContext context, List<Book> books)
        {
            //DB doesn't allow null of any string-values in book-object.
            //fill out possible null-values if any amongst the input-books
            for (int i = 0; i < books.Count; i++)
            {
                books[i].Author = SetTempValIfNull(books[i].Author);
                books[i].Description = SetTempValIfNull(books[i].Description);
                books[i].Genre = SetTempValIfNull(books[i].Genre);
                books[i].Title = SetTempValIfNull(books[i].Title);
            }

            context.AddRange(books);
            context.SaveChanges();
        }

        /// <summary>
        /// Testhelp-method.
        /// Returns "temp" if input is null, otherwise input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static string SetTempValIfNull(string input) => string.IsNullOrEmpty(input) ? "temp" : input;

        /// <summary>
        /// Creates Inmemory db for BooxContext
        /// </summary>
        /// <param name="testBooks">Initial books for the database table</param>
        /// <returns></returns>
        public static BooxContext CreateInMemBooxContext(List<Book> testBooks = null)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var contextOptions = new DbContextOptionsBuilder<BooxContext>()
                .UseSqlite(connection)
                .Options;

            connection.Close();
            connection.Dispose();

            var ctx = new BooxContext(contextOptions);

            //Ensure db is empty.
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            if (testBooks != null)
            {
                AddTestBooksToContext(ctx, testBooks);
            }

            return ctx;
        }
    }
}
