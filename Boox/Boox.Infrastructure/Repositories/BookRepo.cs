using Boox.Core.Interfaces;
using Boox.Core.Models;
using Boox.Core.Models.Entities;
using Boox.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Boox.Infrastructure.Repositories
{
    public class BookRepo : IBookRepo
    {
        private BooxContext _ctx { get; }

        public BookRepo(BooxContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Book> GetAllNoTracking() => _ctx.Books.AsNoTracking();

        public Book GetTrackedById(string id) => _ctx.Books
            .FirstOrDefault(x => x.Id.ToLower().Contains(id.ToLower()) );

        public bool BookExists(string id) 
            => _ctx.Books.Any(x => x.Id.Equals(id));

        public IEnumerable<Book> SortedByAuthor(string name)
        {
            var books = GetAllNoTracking().ToList();

            return string.IsNullOrWhiteSpace(name) 
                ? books.OrderBy(x => x.Author) 
                : books.Where(x => x.Author.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Author);
        }

        public IEnumerable<Book> SortedByDescription(string description)
        {
            var books = GetAllNoTracking();

            return string.IsNullOrWhiteSpace(description)
                ? books.OrderBy(x => x.Description)
                : books.Where(x => x.Description.Contains(description, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Description);
        }

        public IEnumerable<Book> SortedByGenre(string genre)
        {
            var books = GetAllNoTracking();

            return string.IsNullOrWhiteSpace(genre)
                ? books.OrderBy(x => x.Genre)
                : books.Where(x => x.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Genre);
        }

        public IEnumerable<Book> SortedById(string id)
        {
            var books = GetAllNoTracking();
            var comparer = new BookIdComparer();

            return string.IsNullOrWhiteSpace(id)
                ? books.OrderBy(x => x.Id, comparer)
                : books.Where(x => x.Id.Contains(id, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Id, comparer)
                    .ToList();
        }

        public IEnumerable<Book> SortedByPrice(string input)
        {
            var books = GetAllNoTracking();

            if (input != null)
            {
                var inputs = input?
                    .Split('&')
                    .Select(x => double.Parse(x, CultureInfo.InvariantCulture))
                    .ToList();

                switch(inputs.Count())
                {
                    case 0:
                        break;
                    case 1:
                        books = books.Where(x => x.Price == inputs[0]);
                        break;
                    //Anything larger than 1
                    default:
                        books = books.Where(x => x.Price >= inputs[0] &&
                                                x.Price <= inputs[1]);
                        break;
                }
            }

            return books.OrderBy(x => x.Price);

        }

        /// <summary>
        /// Gets books by inputs and orders the result by published-property.
        /// If all input fields are null then all books are returned.
        /// </summary>
        /// <param name="year">Get books published a given year</param>
        /// <param name="month">Get books published a given month.</param>
        /// <param name="day">Get books published a given day.</param>
        /// <returns></returns>
        public IEnumerable<Book> SortedByPublished(int? year, int? month, int? day)
        {
            return GetAllNoTracking().Where(x =>
                (year == null
                    ? true
                    : x.Published.Year == year)
                    && (month == null
                        ? true
                        : x.Published.Month == month)
                        && (day == null
                            ? true
                            : x.Published.Day == day))
                .OrderBy(x => x.Published);
        }

        public IEnumerable<Book> SortedByTitle(string title)
        {
            var books = GetAllNoTracking();

            return string.IsNullOrWhiteSpace(title)
                ? books.OrderBy(x => x.Title)
                : books.Where(x => x.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Title);
        }

        public Book UpdateBook(Book book)
        {
            var updatedBook = _ctx.Books.Update(book);
            _ctx.SaveChanges();
            return updatedBook.Entity;
        }

        public Book PostBook(Book book)
        {
            var newBook = _ctx.Books.Add(book);
            _ctx.SaveChanges();

            return newBook.Entity;
        }
    }
}
