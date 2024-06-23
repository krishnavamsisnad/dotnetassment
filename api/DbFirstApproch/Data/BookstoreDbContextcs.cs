using DbFirstApproch.Models;
using Microsoft.EntityFrameworkCore;
using DbFirstApproch.Data;


namespace DbFirstApproch.Data
{
    public class BookstoreDbContext : DbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Book>()
               .HasOne(b => b.Author)
               .WithMany(a => a.Books)
               .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<BookAuthor>()
                .HasMany(b => b.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(a => a.AuthorId);

        }

    }
}