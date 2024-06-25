using Bookstoreapi_1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstoreapi_1.Repository.RepositryInterface
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Author> GetAuthorAsync(int id);
        Task AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
    }
}
