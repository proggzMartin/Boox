using Boox.Core.Models.Entities;
using Boox.Infrastructure.Data;
using Boox.Infrastructure.Repositories;
using System;
using System.Collections.Generic;

namespace Boox.Tests.UnitTests
{
    /// <summary>
    /// Common methods for BookRepoTest.
    /// Abstract since shouldn't be instantiated
    /// </summary>
    public abstract class AbstractBookRepoTest
    {
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
            var ctx = TestTools.CreateInMemBooxContext(testBooks);
            BooxRepoTestAndAssert(ctx, functionToTest, resultsAction);
        }

        public void BooxRepoTestAndAssert(BooxContext ctx,
            Func<BookRepo, List<Book>> functionToTest,
            Action<List<Book>> resultsAction)
        {
            var repo = new BookRepo(ctx);

            var sortedList = functionToTest(repo);

            resultsAction(sortedList);
        }
    }
}
