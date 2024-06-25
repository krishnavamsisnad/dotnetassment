using Bookstoreapi_1.Models;
using Bookstoreapi_1.Repository.RepositryInterface;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstoreapi_1.Business
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _customerRepository.GetCustomersAsync();
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Customer ID must be greater than zero.");
            }

            return await _customerRepository.GetCustomerAsync(id);
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            await _customerRepository.CreateCustomerAsync(customer);
        }

        public async Task<Customer> UpdateCustomerAsync(int id, Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            if (id != customer.CustomerId)
            {
                throw new ArgumentException("The customer ID does not match the ID in the route.");
            }

            var updatedCustomer = await _customerRepository.UpdateCustomerAsync(customer);
            return updatedCustomer;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Customer ID must be greater than zero.");
            }

            return await _customerRepository.DeleteCustomerAsync(id);
        }
    }
}
