using Microsoft.EntityFrameworkCore;
using Project_1.Models;

namespace Project_1.Data
{
    public class BookstoreDbContextcs:DbContext
    {

        public BookstoreDbContextcs(DbContextOptions<BookstoreDbContextcs> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
               .HasOne(b => b.Author)
               .WithMany(a => a.Books)
               .HasForeignKey(b => b.AuthorId);
            modelBuilder.Entity<BookAuthor>().ToTable("BookAuthors");
        }

    }
}

   
