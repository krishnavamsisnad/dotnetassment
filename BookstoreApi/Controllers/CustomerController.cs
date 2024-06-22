using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_1.Data;
using Task_1.Models;

namespace Task_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly BookstoreDbContext _db;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(BookstoreDbContext db, ILogger<CustomerController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            try
            {
                return Ok(await _db.Customers.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting customers.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting customers.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Customer customers)
        {
            _db.Customers.Add(customers);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomer), new { id =customers.CustomerId }, customers);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Author>> DeletCakes(int id)
        {
            if (_db.Customers == null)
            {
                return NotFound();
            }
            var cak = await _db.Customers.FindAsync(id);
            if (cak == null)
            {
                return NotFound();
            }
            _db.Customers.Remove(cak);
            await _db.SaveChangesAsync();
            return Ok();
        }

    }

    
}