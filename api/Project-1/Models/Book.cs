using Project_1.Models;
using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_1.Models
{
    public class Book
    {
        [Key]
        public required int BookId { get; set; }
        [StringLength(30, ErrorMessage = "The title cannot be longer than 30 characters.")]
        [Sieve(CanFilter = true, CanSort = true)] public string? Title { get; set; }
        [StringLength(30)]
        [Sieve(CanFilter = true, CanSort = true)] public string? Genre { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Publication Date")]
        [Sieve(CanFilter = true, CanSort = true)] public DateTime? PublicationDate { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; } // Foreign key for Author
        public virtual BookAuthor Author { get; set; }
    }
}

