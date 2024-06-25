using Bookstoreapi_1.Business;
using Bookstoreapi_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService;
    private readonly SieveProcessor _sieveProcessor;

    public CustomerController(CustomerService customerService, SieveProcessor sieveProcessor)
    {
        _customerService = customerService;
        _sieveProcessor = sieveProcessor;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersAsync([FromQuery] SieveModel model)
    {
        var customers = await _customerService.GetCustomersAsync();
        var cus = customers.AsQueryable();
        cus = _sieveProcessor.Apply(model, cus);
        var res = await cus.ToListAsync();
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomerAsync(int id)
    {
        if (id == 0)
        {
            return BadRequest("Id is not found");
        }
        var customer = await _customerService.GetCustomerAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }
    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomerAsync([FromBody] Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _customerService.GetCustomerAsync(customer.CustomerId);
        return CreatedAtAction(nameof(GetCustomerAsync), new { id = customer.CustomerId }, customer);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Customer>> UpdateCustomerAsync(int id, Customer customer)
    {
        if (id != customer.CustomerId)
        {
            return BadRequest("Id in route does not match Id in request body");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedCustomer = await _customerService.UpdateCustomerAsync(id, customer);
            if (updatedCustomer == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(updatedCustomer);
        }
        catch (Exception ex)
        {
            // Log the exception (ex)
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomerAsync(int id)
    {
        if (id == 0)
        {
            return BadRequest("Invalid customer ID");
        }

        try
        {
            var result = await _customerService.DeleteCustomerAsync(id);
            if (!result)
            {
                return NotFound("Customer not found");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            // Log the exception (ex)
            return StatusCode(500, "Internal server error");
        }
    }

}

