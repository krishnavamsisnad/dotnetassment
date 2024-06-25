using Bookstoreapi_1.Models;
using Bookstoreapi_1.Repository.RepositryInterface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstoreapi_1.Business
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _bookRepository.GetBooksAsync();
        }

        public async Task<Book> GetBookAsync(int id)
        {
            return await _bookRepository.GetBookAsync(id);
        }

        public async Task CreateBookAsync(Book book)
        {
            await _bookRepository.CreateBookAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            await _bookRepository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
        }
    }
}
