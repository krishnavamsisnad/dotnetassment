using Bookstoreapi_1.Models;


namespace Bookstoreapi_1.Repository.RepositryInterface
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(int id);
        Task CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
    }
}