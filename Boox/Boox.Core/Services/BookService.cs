using AutoMapper;
using Boox.Core.Exceptions;
using Boox.Core.Interfaces;
using Boox.Core.Models.Dtos;
using Boox.Core.Models.Entities;

namespace Boox.Core.Services
{
    /// <summary>
    /// BookService maps Dto -> Entity
    /// Performs Create- and Update-calls for books.
    /// </summary>
    public class BookService : IBookService
    {
        private IMapper _mapper { get; }
        private IBookRepo _bookRepo { get; }

        public BookService(IMapper mapper, IBookRepo bookRepo)
        {
            _mapper = mapper;
            _bookRepo = bookRepo;
        }

        /// <summary>
        /// Update book with specific Id in db.
        /// If Id doesnt exist, nothing happens.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <exception cref="BookNotFoundException">Thrown when book of input Id isn't found.</exception>
        public void BookUpdated(string id, BookDto dto)
        {
            var entity = _bookRepo.GetTrackedById(id);

            if (entity == null)
                throw new BookNotFoundException($"Unable to update Book with Id {id}, it wasn't found");

            var book = _mapper.Map<Book>(dto);

            entity.Author = dto.Author;
            entity.Description = dto.Description;
            entity.Genre = dto.Genre;
            entity.Price = dto.Price;
            entity.Published = dto.Published;
            entity.Title = dto.Title;

            _bookRepo.UpdateBook(entity);
        }

        /// <summary>
        /// Create a new book.
        /// </summary>
        /// <param name="dto"></param>
        public void PostBook(BookDto dto)
        {
            var newbook = _mapper.Map<Book>(dto);
            _bookRepo.PostBook(newbook);
        }
    }
}
