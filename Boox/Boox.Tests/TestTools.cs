using AutoMapper;
using Boox.Core;
using Boox.Core.Models.Entities;
using Boox.Infrastructure.Data;
using Boox.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boox.Tests
{
    public static class TestTools
    {
        public static void AddTestDataToContext(BooxContext context, List<Book> books)
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

        private static BooxContext CreateBooxContext()
        {
            SqliteConnection connection = new SqliteConnection("DataSource=:memory:");
            BooxContext ctx;

            try
            {
                connection.Open();

                var options = new DbContextOptionsBuilder<BooxContext>()
                    .UseSqlite(connection)
                    .Options;

                ctx = new BooxContext(options);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }

            //InMemDb's may cling on in memory if not deleted properly
            //ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            return ctx;
        }

        public static BooxContext CreateInMemBooxContext()
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

            return ctx;
        }
    }
}
