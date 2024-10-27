using System.ComponentModel.DataAnnotations;
using BookHistoryApi.Messages;

namespace BookHistoryApi.Models.Entities
{
    public class Author : Auditable
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.AuthorNameRequired)]
        [StringLength(255)]
        public required string Name { get; set; }
        
        // Navigation property for the many-to-many relationship with books
        public virtual ICollection<BookAuthors> BookAuthors { get; set; } = new List<BookAuthors>();   
    }
}