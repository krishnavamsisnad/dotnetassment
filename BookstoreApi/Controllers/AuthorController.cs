using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_1.Data;
using Task_1.Models;

namespace Task_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase

    {
        private readonly BookstoreDbContext _db;
        private readonly ILogger<BooksController> _logger;
        public AuthorController(BookstoreDbContext db, ILogger<BooksController> logger)
        {
            _db = db;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthor()
        {
            try
            {
                var books = await _db.Authors.ToListAsync();




                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving data from the database.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }
        [HttpPost]
        [Route("AddAuthor")]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            try
            {
                if (author == null)
                {
                    return BadRequest("Author is null.");
                }

                _db.Authors.Add(author);
                await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAuthor), new { id = author.AuthorId }, author);
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
        [HttpPut("{Authorid}")]
        public async Task<ActionResult<Author>> UpdateAuthor(int Authorid, Author author)
        {
            try
            {
                var authors = await _db.Authors.FindAsync(Authorid);
                if (authors == null)
                {
                    return NotFound();
                }

                authors.AuthorId = author.AuthorId;
                author.Name = author.Name;
                _db.Authors.Update(authors);
                await _db.SaveChangesAsync();

                return Ok(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating the author.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the author.");
            }

        }
        [HttpDelete("{Authorid}")]
        public async Task<ActionResult<Author>> RemovAuthor(int Authorid)
        {
            try
            {
                var removeAuthor = await _db.Authors.FindAsync(Authorid);
                if (removeAuthor == null)
                {
                    return NotFound();
                }

                _db.Authors.Remove(removeAuthor);
                await _db.SaveChangesAsync();

                return Ok(removeAuthor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting the removeAuthor.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the removeAuthor.");
            }
        }


    }
   
}
