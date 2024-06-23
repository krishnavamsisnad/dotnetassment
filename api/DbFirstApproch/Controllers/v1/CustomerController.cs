using DbFirstApproch.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbFirstApproch.Models;


namespace DbFirstApproch.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Customer>> GetCustomerId(int id)
        {
            try
            {
                if (_db.Customers == null)
                {
                    return NotFound();
                }
                var custId = await _db.Customers.FindAsync(id);
                if (custId == null)
                {
                    return NotFound();
                }
                return custId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting customer by id");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customers)
        {
            _db.Customers.Add(customers);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomer), new { id = customers.CustomerId }, customers);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeletCakes(int id)
        {
            if (_db.Customers == null)
            {
                return NotFound();
            }
            var removecustomer = await _db.Customers.FindAsync(id);
            if (removecustomer == null)
            {
                return NotFound();
            }
            _db.Customers.Remove(removecustomer);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, Customer customer)
        {
            var seller = await _db.Customers.FindAsync(id);
            if (seller == null)
            {
                return NotFound("Customer not found.");
            }

            seller.CustomerId = customer.CustomerId;
            seller.Name = customer.Name;
            seller.Email = customer.Email;

            _db.Customers.Update(customer);
            await _db.SaveChangesAsync();

            return Ok(seller);
        }
    }

}

