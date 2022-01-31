using Boox.Core.Models.Dtos;

namespace Boox.Core.Interfaces
{
    public interface IBookService
    {
        public void UpdateBook(string id, BookDto dto);
        void PostBook(BookDto book);
    }
}
