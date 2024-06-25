using Bookstoreapi_1.Business;
using Bookstoreapi_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstoreapi_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;
        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorsAsync()
        {
            var author= await _authorService.GetAuthorsAsync();
            return Ok(author);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorAsync(int id)
        {
            if(id == 0)
            {
                return BadRequest("Id not found");
            }
            var aut=await _authorService.GetAuthorAsync(id);
            return Ok(aut);
        }
        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthorAsync(Author author)
        {
            if (author == null)
            {
                return BadRequest("Book is required");
            }

            await _authorService.CreateAuthorAsync(author);

            return CreatedAtAction(nameof(GetAuthorAsync), new { id = author.AuthorId }, author);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> UpdateAuthorAsync( int id,  Author author)
        {
            if(author == null)
            {
                return BadRequest("author not found");
            }
            if(id != author.AuthorId)
            {
                return BadRequest("Book ID does not match");
            }
          await _authorService.UpdateAuthorAsync(author);
            return Ok(author);
            
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Author>> DeleteAuthorAsync(int id)
        {
            if(id == 0)
            {
                return BadRequest("Id is not found");
            }
            await _authorService.DeleteAuthorAsync(id);
            return Ok();

        }
    }

 }

