using Boox.Core.Interfaces;
using Boox.Infrastructure.Data;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Boox.Infrastructure.Repositories
{
    public class BookRepo : IBookRepo
    {
        private BooxContext _ctx { get; }

        public BookRepo(BooxContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Book> GetAll() => _ctx.Books.AsNoTracking();

        public IEnumerable<Book> SortedByAuthor(string name)
        {
            var books = GetAll().ToList();

            return string.IsNullOrWhiteSpace(name) 
                ? books.OrderBy(x => x.Author) 
                : books.Where(x => x.Author.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Author);
        }

        public IEnumerable<Book> SortedByDescription(string description)
        {
            var books = GetAll();

            return string.IsNullOrWhiteSpace(description)
                ? books.OrderBy(x => x.Description)
                : books.Where(x => x.Description.Contains(description, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Description);
        }

        public IEnumerable<Book> SortedByGenre(string genre)
        {
            var books = GetAll();

            return string.IsNullOrWhiteSpace(genre)
                ? books.OrderBy(x => x.Genre)
                : books.Where(x => x.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Genre);
        }

        public IEnumerable<Book> SortedById(string id)
        {
            var books = GetAll();

            return string.IsNullOrWhiteSpace(id)
                ? books.OrderBy(x => x.Id)
                : books.Where(x => x.Id.Contains(id, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Id);
        }

        public IEnumerable<Book> SortedByTitle(string title)
        {
            var books = GetAll();

            return string.IsNullOrWhiteSpace(title)
                ? books.OrderBy(x => x.Title)
                : books.Where(x => x.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Title);
        }
    }
}
