using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DbFirstApproch.Models
{
    public class Customer
    {
        [Required]
        public int CustomerId { get; set; }
        [StringLength(30, ErrorMessage = "The genre cannot be longer than 30 characters.")]
        [Sieve(CanFilter = true, CanSort = true)] public string? Name { get; set; }
        [EmailAddress]
        [Sieve(CanFilter = true, CanSort = true)] public string? Email { get; set; }
 
    }
}
