using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task_1.Data;
using Task_1.Models;

namespace Task_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookstoreDbContext _db;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BookstoreDbContext db, ILogger<BooksController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            try
            {
                var books = await _db.Books.ToListAsync();  
                                    

               

                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving data from the database.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        [HttpPost]
        [Route("addbook")]
        public async Task<ActionResult<Book>> CreateBook([FromQuery] Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("Book is null.");
                }

                // Ensure Author exists or attach existing Author
                if (book.Author != null)
                {
                    var existingAuthor = await _db.Authors.FindAsync(book);
                    if (existingAuthor != null)
                    {
                        book.Author = existingAuthor;
                    }
                    else
                    {
                        return BadRequest("Author does not exist.");
                    }
                }

                _db.Books.Add(book);
                await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBooks), new { id = book.BookId }, book);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving data to the database.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving data to the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, Book book)
        {
            try
            {
                var existingBook = await _db.Books.FindAsync(id);
                if (existingBook == null)
                {
                    return NotFound();
                }

                existingBook.Title = book.Title;
                existingBook.Genre = book.Genre;
                existingBook.PublicationDate = book.PublicationDate;
                existingBook.AuthorId = book.AuthorId;

                _db.Books.Update(existingBook);
                await _db.SaveChangesAsync();

                return Ok(existingBook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating the book.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the book.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            try
            {
                var book = await _db.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                _db.Books.Remove(book);
                await _db.SaveChangesAsync();

                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting the book.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the book.");
            }
        }
    }
}
