using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookHistoryApi.Models.Entities
{
    public class Book : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public required string Title { get; set; }

        [StringLength(1000)]
        public required string ShortDescription { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        [Required]
        public List<int> AuthorIds { get; set; } = new List<int>();
        public virtual ICollection<BookHistory> BookHistories { get; set; } = new List<BookHistory>(); // Add this line
    }
}
