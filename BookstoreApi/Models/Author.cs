using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class Author
    {
        [Key]
        public required int AuthorId { get; set; }
        [StringLength(30)] 
        public required string Name { get; set; }
        //Refrence Navigation Property for Books
        public virtual ICollection<Book> Books { get; set; }=new List<Book>();  
    }
}
