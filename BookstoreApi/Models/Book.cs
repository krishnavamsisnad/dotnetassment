﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_1.Models
{
    public class Book
    {
        [Key]
        public required int BookId { get; set; }
        [StringLength(30, ErrorMessage = "The title cannot be longer than 30 characters.")]
        public string? Title { get; set; }
        [StringLength(30)]
        public string? Genre { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Publication Date")]
        public DateTime? PublicationDate { get; set; }
        [ForeignKey("Author")]
        public  int AuthorId { get; set; } // Foreign key for Author

        public virtual Author? Author { get; set; }
    }
}