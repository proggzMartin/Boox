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

        private IActionResult CommonGetter(Func<IEnumerable<Book>> SpecificAction)
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
            => CommonGetter(() => _booxRepo.GetAll());

        [HttpGet("author/{value?}")]
        public IActionResult GetByAuthor(string? value) 
            => CommonGetter(() => _booxRepo.SortedByAuthor(value));

        [HttpGet("description/{value?}")]
        public IActionResult GetByDescription(string? value) 
            => CommonGetter(() => _booxRepo.SortedByDescription(value));

        [HttpGet("genre/{value?}")]
        public IActionResult GetByGenre(string? value) 
            => CommonGetter(() => _booxRepo.SortedByGenre(value));

        [HttpGet("id/{value?}")]
        public IActionResult GetById(string? value)
            => CommonGetter(() => _booxRepo.SortedById(value));

        //According to specification, input values aren't querystrings, 
        //as it doesn't contain any '?' : https://host:port/api/books/price/30.0&35.0
        //https://www.codeproject.com/Questions/886316/Querystring-qith-out-mark-is-possible-in-asp-Net
        //"The "?" character is the indicator to a URL processor that what follows it is the query detail, and not part of the URL itself."
        [HttpGet("price/{value?}")]
        public IActionResult GetByPrice(string? value)
            => CommonGetter(() => _booxRepo.SortedByPrice(value));

        [HttpGet("published/{year?}/{month?}/{day?}")]
        public IActionResult GetById(int? year, int? month, int? day)
            => CommonGetter(() => _booxRepo.SortedByPublished(year, month, day));

        [HttpGet("title/{value?}")]
        public IActionResult GetByTitle(string? value)
            => CommonGetter(() => _booxRepo.SortedByTitle(value));

    }
}
