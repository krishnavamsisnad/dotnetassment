﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task_1.Data;
using Task_1.Models;

namespace Task_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookstoreDbContext _db;
        private readonly SieveProcessor _processor;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BookstoreDbContext db, ILogger<BooksController> logger ,SieveProcessor processor)
        {
            _db = db;
            _logger = logger;
            _processor = processor;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks([FromQuery] SieveModel model )
        {
            var book = _db.Books.AsQueryable();
            book = _processor.Apply(model, book);
            var books= await book.ToListAsync();
            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookId(int id)
        {
            try
            {
                if (_db.Books == null)
                {
                    return NotFound();
                }
                var bookId = await _db.Books.FindAsync(id);
                if (bookId == null)
                {
                    return NotFound();
                }
                return bookId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting book by id");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost]
        [Route("addbook")]
        public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (book == null)
            {
                return BadRequest("Book is null.");
            }

            if (book.Author != null)
            {
                var existingAuthor = await _db.Authors.FindAsync(book.Author.AuthorId);
                if (existingAuthor != null)
                {
                    book.Author = existingAuthor;
                }
                else
                {
                    return BadRequest("Author does not exist.");
                }
            }

            _db.Books.Add(book);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooks), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, Book book)
        {
            try
            {
                var existingBook = await _db.Books.FindAsync(id);
                if (existingBook == null)
                {
                    return NotFound();
                }

                existingBook.Title = book.Title;
                existingBook.Genre = book.Genre;
                existingBook.PublicationDate = book.PublicationDate;
                existingBook.AuthorId = book.AuthorId;

                _db.Books.Update(existingBook);
                await _db.SaveChangesAsync();

                return Ok(existingBook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating the book.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the book.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            try
            {
                var book = await _db.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                _db.Books.Remove(book);
                await _db.SaveChangesAsync();

                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting the book.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the book.");
            }
        }
    }
}
