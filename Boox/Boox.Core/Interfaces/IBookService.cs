using Boox.Core.Models.Dtos;

namespace Boox.Core.Interfaces
{
    public interface IBookService
    {
        void BookUpdated(string id, BookDto dto);
        void PostBook(BookDto book);
    }
}
