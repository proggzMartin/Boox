using Boox.Core.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Boox.Web.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private IBookRepo _booxRepo { get; }

        public BooksController(IBookRepo booxRepo)
        {
            _booxRepo = booxRepo;
        }

        private IActionResult CommonGetter<T>(Func<IEnumerable<Book>> SpecificAction, params T[] args)
        {
            try
            {
                return Ok(SpecificAction());
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Get() 
            => CommonGetter<string>(() => _booxRepo.GetAll());

        [HttpGet("author/{value?}")]
        public IActionResult GetByAuthor(string? value) 
            => CommonGetter<string>(() => _booxRepo.SortedByAuthor(value));

        [HttpGet("description/{value?}")]
        public IActionResult GetByDescription(string? value) 
            => CommonGetter<string>(() => _booxRepo.SortedByDescription(value));

        [HttpGet("genre/{value?}")]
        public IActionResult GetByGenre(string? value) 
            => CommonGetter<string>(() => _booxRepo.SortedByGenre(value));

        [HttpGet("Id/{value?}")]
        public IActionResult GetById(string? value)
            => CommonGetter<string>(() => _booxRepo.SortedById(value));

        [HttpGet("Title/{value?}")]
        public IActionResult GetByTitle(string? value)
            => CommonGetter<string>(() => _booxRepo.SortedByTitle(value));

    }
}
