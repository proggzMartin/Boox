using AutoMapper;
using Boox.Core.Interfaces;
using Boox.Core.Models.Dtos;
using Boox.Core.Models.Entities;

namespace Boox.Core.Services
{
    public class BookService : IBookService
    {
        private IMapper _mapper { get; }
        private IBookRepo _bookRepo { get; }

        public BookService(IMapper mapper, IBookRepo bookRepo)
        {
            _mapper = mapper;
            _bookRepo = bookRepo;
        }

        public void UpdateBook(string id, BookDto dto)
        {
            var entity = _bookRepo.GetTrackedById(id);

            var book = _mapper.Map<Book>(dto);
            
            entity.Author = dto.Author;
            entity.Description = dto.Description;
            entity.Genre = dto.Genre;
            entity.Price = dto.Price;
            entity.Published = dto.Published;
            entity.Title = dto.Title;

            _bookRepo.UpdateBook(entity);
        }

        public void PostBook(BookDto dto)
        {
            var newbook = _mapper.Map<Book>(dto);
            _bookRepo.PostBook(newbook);
        }
    }
}
