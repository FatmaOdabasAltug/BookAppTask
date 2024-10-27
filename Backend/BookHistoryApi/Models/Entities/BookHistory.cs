using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookHistoryApi.Models.Entities
{
    public class BookHistory : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public virtual Book? Book { get; set; } // Navigation property

        [Required]
        public required string ChangeDescription { get; set; }
    }
}
