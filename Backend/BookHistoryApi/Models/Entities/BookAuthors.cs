using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookHistoryApi.Models.Entities
{
    public class BookAuthors
    {
        // Primary key part 1: The ID of the associated Book
        [Key, Column(Order = 0)]  // Specifies this property as part of a composite key and its order
        public int BookId { get; set; }

        // Navigation property for the associated Book entity
        [ForeignKey(nameof(BookId))]
        public virtual Book? Book { get; set; }

        // Primary key part 2: The ID of the associated Author
        [Key, Column(Order = 1)] 
        public int AuthorId { get; set; }

        // Navigation property for the associated Author entity
        [ForeignKey(nameof(AuthorId))]
        public virtual Author? Author { get; set; }
    }
}