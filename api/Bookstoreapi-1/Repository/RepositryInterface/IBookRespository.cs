using Bookstoreapi_1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstoreapi_1.Repository.RepositryInterface
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(int id);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}