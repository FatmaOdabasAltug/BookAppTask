using BookHistoryApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookHistoryApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties for each entity
        public DbSet<Book> Books { get; set; } // Represents the Books table
        /*public DbSet<Author> Authors { get; set; } // Represents the Authors table
        public DbSet<BookAuthors> BookAuthors { get; set; } // Represents the BookAuthors junction table*/
        public DbSet<BookHistory> BookHistories { get; set; } // Represents the BookHistory table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the composite key for BookAuthors
            modelBuilder.Entity<BookAuthors>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId }); // Composite key

            // Configuring the relationships for BookAuthors
            /*modelBuilder.Entity<BookAuthors>()
                .HasOne(ba => ba.Book) // Each BookAuthors entry is associated with one Book
                .WithMany(b => b.BookAuthors) // Each Book can have many BookAuthors entries
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthors>()
                .HasOne(ba => ba.Author) // Each BookAuthors entry is associated with one Author
                .WithMany(a => a.BookAuthors) // Each Author can have many BookAuthors entries
                .HasForeignKey(ba => ba.AuthorId);*/

            // Configuring the relationships for BookHistory
            modelBuilder.Entity<BookHistory>()
                .HasOne(ch => ch.Book) // Each BookHistory entry is associated with one Book
                .WithMany(b => b.BookHistories) // Each Book can have many BookHistories entries
                .HasForeignKey(ch => ch.BookId);
        }
    }
}
