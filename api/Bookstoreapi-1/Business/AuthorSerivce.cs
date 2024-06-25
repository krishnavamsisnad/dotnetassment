using Bookstoreapi_1.Models;
using Bookstoreapi_1.Repository.RepositryInterface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstoreapi_1.Business
{
    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _authorRepository.GetAuthorsAsync();
        }

        public async Task<Author> GetAuthorAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Author ID must be greater than zero", nameof(id));
            }

            return await _authorRepository.GetAuthorAsync(id);
        }

        public async Task CreateAuthorAsync(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            await _authorRepository.AddAuthorAsync(author);
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            await _authorRepository.UpdateAuthorAsync(author);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Author ID must be greater than zero", nameof(id));
            }

            await _authorRepository.DeleteAuthorAsync(id);
        }
    }
}