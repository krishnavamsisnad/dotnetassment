using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class Author
    {
        [Key]
        [Sieve(CanFilter = true, CanSort = true)] public required int AuthorId { get; set; }
        [StringLength(30)]
        [Sieve(CanFilter = true, CanSort = true)] public required string Name { get; set; }
        //Refrence Navigation Property for Books
        public virtual ICollection<Book> Books { get; set; }=new List<Book>();  
    }
}
