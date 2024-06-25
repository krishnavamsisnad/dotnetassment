using Bookstoreapi_1.Data;
using Bookstoreapi_1.Models;
using Bookstoreapi_1.Repository.RepositryInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstoreapi_1.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BookstoreDbContext _context;

        public CustomerRepository(BookstoreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            var existingCustomer = await _context.Customers.FindAsync(customer.CustomerId);
            if (existingCustomer == null)
            {
                return null; // Or throw an exception if preferred
            }

            // Update the existing customer properties with the provided customer data
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            // ... update other properties as needed

            await _context.SaveChangesAsync();

            return existingCustomer;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
