using BookHistoryApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookHistoryApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // DbSet properties for each entity
        public DbSet<Book> Books { get; set; } // Represents the Books table
        public DbSet<BookHistory> BookHistories { get; set; } // Represents the BookHistory table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuring the relationships for BookHistory
            modelBuilder
                .Entity<BookHistory>()
                .HasOne(ch => ch.Book) // Each BookHistory entry is associated with one Book
                .WithMany(b => b.BookHistories) // Each Book can have many BookHistories entries
                .HasForeignKey(ch => ch.BookId);
        }
    }
}
