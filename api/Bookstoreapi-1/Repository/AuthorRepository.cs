using Bookstoreapi_1.Data;
using Bookstoreapi_1.Models;
using Bookstoreapi_1.Repository.RepositryInterface;
using Microsoft.EntityFrameworkCore;

namespace Bookstoreapi_1.Business.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookstoreDbContext _dbContext;

        public AuthorRepository(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Author?> GetAuthorAsync(int authorId)
        {
            return await _dbContext.Authors.FindAsync(authorId);
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _dbContext.Authors.ToListAsync();
        }

        public async Task AddAuthorAsync(Author author)
        {
            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            _dbContext.Authors.Update(author);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int authorId)
        {
            var author = await GetAuthorAsync(authorId);
            if (author != null)
            {
                _dbContext.Authors.Remove(author);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
