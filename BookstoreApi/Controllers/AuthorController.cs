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
        public async Task<ActionResult<IEnumerable<Author>>> GetBooks()
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
    }
}
