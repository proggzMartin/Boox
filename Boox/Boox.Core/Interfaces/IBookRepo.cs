using Core.Entities;

namespace Boox.Core.Interfaces
{
    public interface IBookRepo
    {
        public IEnumerable<Book> GetAll();
        public IEnumerable<Book> SortedByAuthor(string name);
        public IEnumerable<Book> SortedByDescription(string name);
        public IEnumerable<Book> SortedByGenre(string name);
        public IEnumerable<Book> SortedById(string name);
        public IEnumerable<Book> SortedByTitle(string name);
    }
}
