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
    }
}