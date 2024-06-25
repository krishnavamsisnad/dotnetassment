using System.ComponentModel.DataAnnotations;

namespace Bookstoreapi_1.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
