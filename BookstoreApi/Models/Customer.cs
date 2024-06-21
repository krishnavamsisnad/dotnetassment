using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class Customer
    {
        [Required]
       public int CustomerId { get; set; }
        [StringLength(30, ErrorMessage ="The genre cannot be longer than 30 characters.")]
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}
