using System.ComponentModel.DataAnnotations;

namespace Bookstoreapi_1.Models
{
    public class Author
    {
        [Key]
        public required int AuthorId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
