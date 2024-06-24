using Project_1.Models;
using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Project_1.Models
{
    public class Author
    {
        [Key]
        [Sieve(CanFilter = true, CanSort = true)] public required int AuthorId { get; set; }
        [StringLength(30)]
        [Sieve(CanFilter = true, CanSort = true)] public required string Name { get; set; }
        //Refrence Navigation Property for Books
        public  ICollection<Book> Books { get; set; }
    }

}
