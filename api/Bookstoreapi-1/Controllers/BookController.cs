using Microsoft.AspNetCore.Mvc;
using Bookstoreapi_1.Business;
using Bookstoreapi_1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstoreapi_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        /// <summary>
        /// GET /analytics/sales/summary
        /// Get sales summary.
        /// </summary>
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _bookService.GetBooksAsync();
            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Book ID must be greater than zero");
            }

            var book = await _bookService.GetBookAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            if (book == null)
            {
                return BadRequest("Book is required");
            }

            await _bookService.CreateBookAsync(book);

            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (id <= 0)
            {
                return BadRequest("Book ID must be greater than zero");
            }

            if (book == null)
            {
                return BadRequest("Book is required");
            }

            if (id != book.BookId)
            {
                return BadRequest("Book ID does not match");
            }

            await _bookService.UpdateBookAsync(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Book ID must be greater than zero");
            }

            await _bookService.DeleteBookAsync(id);

            return Ok(id);
        }
    }
}
