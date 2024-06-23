using DbFirstApproch.Data;
using DbFirstApproch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace DbFirstApproch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly BookstoreDbContext _db;
        private readonly SieveProcessor _processor;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(BookstoreDbContext db, ILogger<AuthorController> logger, SieveProcessor processor)
        {
            _db = db;
            _logger = logger;
            _processor = processor;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BookAuthor>>> GetAuthors([FromQuery] SieveModel model)
        {
            var authors = _db.BookAuthors.AsQueryable();
            authors = _processor.Apply(model, authors);
            var result = await authors.ToArrayAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookAuthor>> GetAuthorById(int id)
        {
            try
            {
                if (_db.BookAuthors == null)
                {
                    return NotFound();
                }
                var author = await _db.BookAuthors.FindAsync(id);
                if (author == null)
                {
                    return NotFound();
                }
                return Ok(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting author by id");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost]
        [Route("authors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<BookAuthor>> CreateAuthor(BookAuthor author)
        {
            try
            {
                if (author == null)
                {
                    return BadRequest("Author is null.");
                }

                _db.BookAuthors.Add(author);
                await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAuthors), new { id = author.AuthorId }, author);
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
        [HttpDelete("{authorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<BookAuthor>> DeleteAuthor(int authorId)
        {
            try
            {
                var author = await _db.BookAuthors.FindAsync(authorId);
                if (author == null)
                {
                    return NotFound();
                }

                _db.BookAuthors.Remove(author);
                await _db.SaveChangesAsync();

                return Ok(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting the author.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the author.");
            }
        }

        [HttpPut("{authorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookAuthor>> UpdateAuthor(int authorId, BookAuthor author)
        {
            try
            {
                var existingAuthor = await _db.BookAuthors.FindAsync(authorId);
                if (existingAuthor == null)
                {
                    return NotFound();
                }

                existingAuthor.Name = author.Name;
                _db.BookAuthors.Update(existingAuthor);
                await _db.SaveChangesAsync();

                return Ok(existingAuthor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating the author.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the author.");
            }
        }


    }
}
