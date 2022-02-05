using Boox.Core.Models.Entities;
using Boox.Infrastructure.Data;
using Boox.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Boox.Tests.UnitTests
{
    public abstract class AbstractRepoTest : IDisposable
    {
        protected static readonly DbConnection _connection;
        protected static readonly DbContextOptions<BooxContext> _contextOptions;

        static AbstractRepoTest()
        {
            //Create a fresh db for each test.
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<BooxContext>()
                .UseSqlite(_connection)
                .Options;

            _connection.Close();
            _connection.Dispose();

            var ctx = new BooxContext(_contextOptions);

            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
        }
        public void Dispose() => _connection.Dispose();

        public BooxContext CreateBooxContext(List<Book> testBooks = null)
        {
            var ctx = TestTools.CreateInMemBooxContext();

            if (testBooks != null)
            {
                TestTools.AddTestDataToContext(ctx, testBooks);
            }

            return ctx;
        }

        public void BooxRepoTestAndAssert(BooxContext ctx,
            Func<BookRepo, List<Book>> functionToTest,
            Action<List<Book>> resultsAction)
        {
            var repo = new BookRepo(ctx);

            var sortedList = functionToTest(repo);

            resultsAction(sortedList);
        }

        /// <summary>
        /// Method for testing BookRepo
        /// </summary>
        /// <param name="testBooks">Set of books to be tested. If null values exist, they will
        /// be replaced with temporary values.</param>
        /// <param name="functionToTest">The function to be tested. Returns list used in resultsAction-parameter</param>
        /// <param name="resultsAction">Function using result fron functionToTest-parameterm e.g. to perform Asserts</param>
        public void BookRepoTestContainer(List<Book> testBooks,
            Func<BookRepo, List<Book>> functionToTest,
            Action<List<Book>> resultsAction)
        {
            var ctx = CreateBooxContext(testBooks);
            BooxRepoTestAndAssert(ctx, functionToTest, resultsAction);
        }
    }
}
