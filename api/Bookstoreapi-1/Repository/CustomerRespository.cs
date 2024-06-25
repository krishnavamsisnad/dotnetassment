using Bookstoreapi_1.Data;
using Bookstoreapi_1.Models;
using Bookstoreapi_1.Repository.RepositryInterface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstoreapi_1.Business.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BookstoreDbContext _dbContext;

        public CustomerRepository(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerAsync(int CustomerId)
        {
            return await _dbContext.Customers.FindAsync(CustomerId);
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}