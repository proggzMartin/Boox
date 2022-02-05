using Boox.Core.Models.Entities;
using Boox.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Boox.Tests.UnitTests
{
    public class BookRepoTests : AbstractRepoTest
    {
        #region SortAuthor
        /// <summary>
        /// Use case:
        /// Whatever field I ask for, it should return the result sorted by that field.
        /// -> Author
        /// </summary>
        [Fact]
        public void SortedByAuthor_1_InputNull_OrderAll()
        {
            List<Book> testBooks = new List<Book>
            {
                new Book { Author = "C" },
                new Book { Author = "B" },
                new Book { Author = "D" },
                new Book { Author = "A" },
            };


            BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByAuthor(null).ToList(),
                resultsAction: (books) =>
                {
                    Assert.Equal("A", books[0].Author);
                    Assert.Equal("B", books[1].Author);
                    Assert.Equal("C", books[2].Author);
                    Assert.Equal("D", books[3].Author);
                });
        }

        /// <summary>
        /// Use case:
        /// I should be able to ask for an author, a title, a genre, or a description.
        /// It should perform the search "case insensitive" and with partial strings.
        /// So, if I ask for "/api/books/author/kim" it should return only the book by "Ralls, Kim".
        /// --> Author
        /// </summary>
        [Fact]
        public void SortedByAuthor_2_Input_CaseInsenitiveAndPartial()
        {
            var auth1 = "auth1 abc";
            var auth2 = "AUTH2 xyz";
            var other1 = "other1";
            var other2 = "OTHER2";

            List<Book> testBooks = new List<Book>
            {
                new Book { Author = auth2 }, //note: auth2 on position 0
                new Book { Author = auth1 },
                new Book { Author = other1 },
                new Book { Author = other2 },
            };

            BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByAuthor("uTH").ToList(),
                resultsAction: (books) =>
                {
                    var authors = books.Select(x => x.Author).ToList();
                    Assert.Contains(auth2, authors);
                    Assert.Contains(auth1, authors);
                    Assert.Equal(auth1, authors[0]);
                    Assert.Equal(auth2, authors[1]); //auth2 now on position 1
                    Assert.DoesNotContain(other1, authors);
                    Assert.DoesNotContain(other2, authors);
                });
        }
        #endregion

        #region SortDescription
        [Fact]
        public void SortedByDescription_1_InputNull_OrderAll()
        {
            List<Book> testBooks = new List<Book>
            {
                new Book { Description = "C" },
                new Book { Description = "B" },
                new Book { Description = "D" },
                new Book { Description = "A" },
            };

            BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByDescription(null).ToList(),
                resultsAction: (books) =>
                {
                    Assert.Equal("A", books[0].Description);
                    Assert.Equal("B", books[1].Description);
                    Assert.Equal("C", books[2].Description);
                    Assert.Equal("D", books[3].Description);
                });
        }


        /// <summary>
        /// Use case:
        /// I should be able to ask for an author, a title, a genre, or a description.
        /// It should perform the search "case insensitive" and with partial strings.
        /// So, if I ask for "/api/books/author/kim" it should return only the book by "Ralls, Kim".
        /// --> Author
        /// </summary>
        [Fact]
        public void SortedByDescription_2_Input_CaseInsenitiveAndPartial()
        {
            var desc1 = "DESC1 NAME1";
            var desc2 = "dEsC2 NAME2";
            var other1 = "other1";
            var other2 = "OTHER2";

            List<Book> testBooks = new List<Book>
            {
                new Book { Description = desc2 }, //desc2 on pos 0
                new Book { Description = desc1 },
                new Book { Description = other1 },
                new Book { Description = other2 },
            };

            BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByDescription("sC").ToList(),
                resultsAction: (books) =>
                {
                    var descriptions = books.Select(x => x.Description).ToList();
                    Assert.Contains(desc1, descriptions);
                    Assert.Contains(desc2, descriptions);
                    Assert.Equal(desc1, descriptions[0]);
                    Assert.Equal(desc2, descriptions[1]);  //desc2 on pos 1 now.
                    Assert.DoesNotContain(other1, descriptions);
                    Assert.DoesNotContain(other2, descriptions);
                });
        }
        #endregion

        #region SortGenre
        [Fact]
        public void SortedByGenre_1_InputNull_OrderAll()
        {
            List<Book> testBooks = new List<Book>
            {
                new Book { Genre = "C" },
                new Book { Genre = "B" },
                new Book { Genre = "D" },
                new Book { Genre = "A" },
            };

            BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByGenre(null).ToList(),
                resultsAction: (books) =>
                {
                    Assert.Equal("A", books[0].Genre);
                    Assert.Equal("B", books[1].Genre);
                    Assert.Equal("C", books[2].Genre);
                    Assert.Equal("D", books[3].Genre);
                });
        }


        /// <summary>
        /// Use case:
        /// I should be able to ask for an author, a title, a genre, or a description.
        /// It should perform the search "case insensitive" and with partial strings.
        /// So, if I ask for "/api/books/author/kim" it should return only the book by "Ralls, Kim".
        /// --> Author
        /// </summary>
        [Fact]
        public void SortedByGenre_2_Input_CaseInsenitiveAndPartial()
        {
            var gen1 = "GEN1 NAME1";
            var gen2 = "geN2 NAME2";
            var other1 = "other1";
            var other2 = "OTHER2";

            List<Book> testBooks = new List<Book>
            {
                new Book { Genre = gen2 }, //gen2 on pos 0
                new Book { Genre = gen1 },
                new Book { Genre = other1 },
                new Book { Genre = other2 },
            };

            BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByGenre("gEn").ToList(),
                resultsAction: (books) =>
                {
                    var genres = books.Select(x => x.Genre).ToList();
                    Assert.Contains(gen1, genres);
                    Assert.Contains(gen2, genres);
                    Assert.Equal(gen1, genres[0]);
                    Assert.Equal(gen2, genres[1]);  //desc2 on pos 1 now.
                    Assert.DoesNotContain(other1, genres);
                    Assert.DoesNotContain(other2, genres);
                });
        }
        #endregion

        #region SortTitle
        [Fact]
        public void SortedByTitle_1_InputNull_OrderAll()
        {
            List<Book> testBooks = new List<Book>
            {
                new Book { Title = "C" },
                new Book { Title = "B" },
                new Book { Title = "D" },
                new Book { Title = "A" },
            };

            BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByTitle(null).ToList(),
                resultsAction: (books) =>
                {
                    Assert.Equal("A", books[0].Title);
                    Assert.Equal("B", books[1].Title);
                    Assert.Equal("C", books[2].Title);
                    Assert.Equal("D", books[3].Title);
                });
        }


        /// <summary>
        /// Use case:
        /// I should be able to ask for an author, a title, a genre, or a description.
        /// It should perform the search "case insensitive" and with partial strings.
        /// So, if I ask for "/api/books/author/kim" it should return only the book by "Ralls, Kim".
        /// --> Author
        /// </summary>
        [Fact]
        public void SortedByTitle_2_Input_CaseInsenitiveAndPartial()
        {
            var title1 = "TITLE1 NAME1";
            var title2 = "TitlE2 NAME2";
            var other1 = "other1";
            var other2 = "OTHER2";

            List<Book> testBooks = new List<Book>
            {
                new Book { Title = title2 }, //title2 on pos 0
                new Book { Title = title1 },
                new Book { Title = other1 },
                new Book { Title = other2 },
            };

            BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByTitle("Title").ToList(),
                resultsAction: (books) =>
                {
                    var genres = books.Select(x => x.Title).ToList();
                    Assert.Contains(title1, genres);
                    Assert.Contains(title2, genres);
                    Assert.Equal(title1, genres[0]);
                    Assert.Equal(title2, genres[1]);  //title2 on pos 1 now.
                    Assert.DoesNotContain(other1, genres);
                    Assert.DoesNotContain(other2, genres);
                });
        }
        #endregion

        #region SortPrices
        [Fact]
        public void SortedByPrice_1_InputNull_OrderAll()
        {
            var price0 = 34;
            var price1 = 34.1;
            var price2 = 34.2;
            var price3 = 38;

            List<Book> testBooks = new List<Book>
            {
                new Book { Price = price3 },
                new Book { Price = price1 },
                new Book { Price = price0 },
                new Book { Price = price2 },
            };

            BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByPrice(null).ToList(),
                resultsAction: (books) =>
                {
                    Assert.Equal(4, books.Count());
                    Assert.Equal(price0, books[0].Price);
                    Assert.Equal(price1, books[1].Price);
                    Assert.Equal(price2, books[2].Price);
                    Assert.Equal(price3, books[3].Price);
                });
        }

        [Fact]
        public void SortedByPrice_2_InputSingleValue_GetAllSamePrice()
        {
            List<Book> testBooks = new List<Book>
            {
                new Book { Price = 39.9 },
                new Book { Price = 34.0 },
                new Book { Price = 34.1 },
                new Book { Price = 34 },
            };

            //Assert getting single OK
            BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByPrice("39.9").ToList(),
                resultsAction: (books) =>
                {
                    Assert.Single(books);
                    Assert.Equal(39.9, books[0].Price);
                });

            //Assert getting Multiple OK
            //Assert getting on different formats
            foreach (var testString in new string[] { "34.0", "34" })
            {
                BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByPrice(testString).ToList(),
                resultsAction: (books) =>
                {
                    Assert.Equal(2, books.Count());
                    Assert.Equal(34.0, books[0].Price);
                    Assert.Equal(34.0, books[1].Price);
                });
            }
        }

        [Fact]
        public void SortedByPrice_3_InputInterval_GetPricesInInterval()
        {
            List<Book> testBooks = new List<Book>
            {
                new Book { Price = 39.9 },
                new Book { Price = 34.0 },
                new Book { Price = 34.1 },
                new Book { Price = 34 },
            };

            //Assert getting Multiple OK
            //Assert getting on different formats
            foreach (var testString in new string[] { "0&99", "34&40", "34.0&40" })
            {
                BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByPrice(testString).ToList(),
                resultsAction: (books) =>
                {
                    Assert.Equal(4, books.Count());
                    Assert.Equal(34.0, books[0].Price);
                    Assert.Equal(34.0, books[1].Price);
                    Assert.Equal(34.1, books[2].Price);
                    Assert.Equal(39.9, books[3].Price);
                });
            }

            //interval below and middle of testset
            BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByPrice("26&35").ToList(),
                resultsAction: (books) =>
                {
                    Assert.Equal(3, books.Count());
                    Assert.Equal(34.0, books[0].Price);
                    Assert.Equal(34.0, books[1].Price);
                    Assert.Equal(34.1, books[2].Price);
                });

            BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByPrice("34.1&52").ToList(),
                resultsAction: (books) =>
                {
                    Assert.Equal(2, books.Count());
                    Assert.Equal(34.1, books[0].Price);
                    Assert.Equal(39.9, books[1].Price);
                });
        }
        #endregion

        #region SortPublishedDate
        [Fact]
        public void SortedByPublishedDate_1_InputNull_OrderAll()
        {
            //3 on same month, 2 on same date.
            var date0 = new DateTime(2022, 01, 01);
            var date1 = new DateTime(2022, 01, 01);
            var date2 = new DateTime(2022, 01, 02);

            var date3 = new DateTime(2022, 02, 01);

            var date4 = new DateTime(2021, 01, 01);
            var date5 = new DateTime(2021, 02, 01);

            List<Book> testBooks = new List<Book>
            {
                new Book { Published = date3 },
                new Book { Published = date1 },
                new Book { Published = date0 },
                new Book { Published = date2 },
                new Book { Published = date4 },
                new Book { Published = date5 },
            };

            BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByPublished(null,null,null).ToList(),
                resultsAction: (books) =>
                {
                    //get all books.
                    Assert.Equal(testBooks.Count(), books.Count());

                    //resulting book should be the same as testBooks, and ordered.
                    var orderedTestbooks = testBooks.OrderBy(x => x.Published).ToList();

                    for(int i = 0; i < books.Count; i++)
                        Assert.Equal(orderedTestbooks[i].Published, books[i].Published);
                });
        }

        [Fact]
        public void SortedByPublishedDate_2_InputNull_OrderAll()
        {
            var date0 = new DateTime(2022, 1, 1);
            var date1 = new DateTime(2022, 1, 1);
            var date2 = new DateTime(2022, 1, 2);
            var date3 = new DateTime(2022, 2, 1);
            var date4 = new DateTime(2021, 1, 1);
            var date5 = new DateTime(2021, 2, 1);

            List<Book> testBooks = new List<Book>
            {
                new Book { Published = date3 },
                new Book { Published = date1 },
                new Book { Published = date0 },
                new Book { Published = date2 },
                new Book { Published = date4 },
                new Book { Published = date5 },
            };

            var x = new List<DateTime> { new DateTime(2022, 1, 1) };

            Action<List<Book>> asserts = (books) => Console.WriteLine();

            //List of combined input year-month-day with corresponding asserts
            //for testing. See list above for database values
            var InputsAndAsserts = new List<((int? year,int? month,int? day) date, Action<List<Book>> assertActions)>
            {
                (
                    date: (2022,null,null),
                    assertActions: (books) => {
                        var bookDates = books.Select(x => x.Published);
                        Assert.Equal(4, bookDates.Count());
                        Assert.Contains(date0, bookDates);
                        Assert.Contains(date1, bookDates);
                        Assert.Contains(date2, bookDates);
                        Assert.Contains(date3, bookDates);
                        Assert.DoesNotContain(date4, bookDates);
                        Assert.DoesNotContain(date5, bookDates);
                    } 
                ), (
                    date: (2021,null,null),
                    assertActions: (books) => {
                        var bookDates = books.Select(x => x.Published);
                        Assert.Equal(2, bookDates.Count());
                        Assert.DoesNotContain(date0, bookDates);
                        Assert.DoesNotContain(date1, bookDates);
                        Assert.DoesNotContain(date2, bookDates);
                        Assert.DoesNotContain(date3, bookDates);
                        Assert.Contains(date4, bookDates);
                        Assert.Contains(date5, bookDates);
                    }
                ), (
                    date: (2022,1,null),
                    assertActions: (books) => {
                        var bookDates = books.Select(x => x.Published);
                        Assert.Equal(3, bookDates.Count());
                        Assert.Contains(date0, bookDates);
                        Assert.Contains(date1, bookDates);
                        Assert.Contains(date2, bookDates);
                        Assert.DoesNotContain(date3, bookDates);
                        Assert.DoesNotContain(date4, bookDates);
                        Assert.DoesNotContain(date5, bookDates);
                    }
                ), (
                    date: (2022,1,1),
                    assertActions: (books) => {
                        var bookDates = books.Select(x => x.Published);
                        Assert.Equal(2, bookDates.Count());
                        Assert.Contains(date0, bookDates);
                        Assert.Contains(date1, bookDates);
                        Assert.DoesNotContain(date2, bookDates);
                        Assert.DoesNotContain(date3, bookDates);
                        Assert.DoesNotContain(date4, bookDates);
                        Assert.DoesNotContain(date5, bookDates);
                    }
                ), (
                    date: (2021,2,null),
                    assertActions: (books) => {
                        var bookDates = books.Select(x => x.Published);
                        Assert.Equal(1, bookDates.Count());
                        Assert.DoesNotContain(date0, bookDates);
                        Assert.DoesNotContain(date1, bookDates);
                        Assert.DoesNotContain(date2, bookDates);
                        Assert.DoesNotContain(date3, bookDates);
                        Assert.DoesNotContain(date4, bookDates);
                        Assert.Contains(date5, bookDates);
                    }
                )
            };

            foreach(var inputAndAssert in InputsAndAsserts)
            {
                BookRepoTestContainer(testBooks,
                functionToTest: (repo) => repo.SortedByPublished(
                                                inputAndAssert.date.year,
                                                inputAndAssert.date.month,
                                                inputAndAssert.date.day).ToList(),
                resultsAction: (books) => inputAndAssert.assertActions(books));
            }
        }
        #endregion
        //I should be able to ask for a price range or a specific price.

        //I should be able to ask for published_date or part of it, that means all books,
        //books from a certain year, books from a certain year-month or books from a
        //certain year-month-day.

        //I should be able to edit any field for any book using the book ID as a search parameter.

        //I should be able to create a new book.
    }
}
