using Bookstoreapi_1.Data;
using Bookstoreapi_1.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookstoreapi_1.Repository.RepositryInterface;

namespace Bookstoreapi_1.Business.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreDbContext _dbContext;

        public BookRepository(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book> GetBookAsync(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                throw new Exception($"Book with ID {id} not found.");
            }
            return book;
        }
        public async Task CreateBookAsync(Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
