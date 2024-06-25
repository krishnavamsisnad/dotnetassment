using Bookstoreapi_1.Models;
using Bookstoreapi_1.Repository.RepositryInterface;
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
                throw new ArgumentException("Customer ID must be greater than zero", nameof(id));
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

        public async Task UpdateCustomerAsync(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Customer ID must be greater than zero", nameof(id));
            }

            await _customerRepository.DeleteCustomerAsync(id);
        }
    }
}
