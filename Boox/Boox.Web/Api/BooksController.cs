using Boox.Core.Exceptions;
using Boox.Core.Interfaces;
using Boox.Core.Models.Dtos;
using Boox.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Boox.Web.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private IBookRepo _booxRepo { get; }
        private IBookService _bookService { get; }

        public BooksController(IBookRepo bookRepo, IBookService bookService)
        {
            _booxRepo = bookRepo;
            _bookService = bookService;
        }

        /// <summary>
        /// Common method for get-methods, in attempt to
        /// avoid copypaste-code and shrink amount of code.
        /// </summary>
        /// <param name="specificAction"></param>
        /// <returns></returns>
        private IActionResult CommonGetter(
            Func<IEnumerable<Book>> specificAction)
        {
            try
            {
                return Ok(specificAction());
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Get()
            => CommonGetter(() => _booxRepo.GetAllNoTracking());

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
        //doesn't contain any '?' e.g. https://host:port/api/books/price/30.0&35.0
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

        [HttpPut("{id}")]
        public IActionResult PutBook(string id, [FromBody] BookDto book)
        {
            try
            {
                _bookService.BookUpdated(id, book);
            }
            catch (BookNotFoundException ex)
            {
                return NotFound(ex.Message);
            } catch 
            {
                return StatusCode(500, "Server error");
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult PostBook([FromBody] BookDto book)
        {
            _bookService.PostBook(book);

            return Ok();
        }
    }
}
