using Bookstoreapi_1.Models;
using System.ComponentModel.DataAnnotations;

namespace Bookstoreapi_1.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}