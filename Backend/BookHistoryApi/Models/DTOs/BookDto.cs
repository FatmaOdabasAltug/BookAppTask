using System.ComponentModel.DataAnnotations;
using BookHistoryApi.Messages;
using BookHistoryApi.Models.Validation;

namespace BookHistoryApi.Models.DTOs
{
    public class BookDto : AuditableDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = ErrorMessages.TitleRequired)]
        [StringLength(255, ErrorMessage = ErrorMessages.TitleMaxLength)]
        [NotNullOrWhiteSpace(ErrorMessage = ErrorMessages.TitleNotNullOrWhiteSpace)]
        public required string Title { get; set; }

        [StringLength(1000, ErrorMessage = ErrorMessages.ShortDescriptionMaxLength)]
        public string? ShortDescription { get; set; } 

        [Required(ErrorMessage = ErrorMessages.PublishDateRequired)]
        [ValidDate]
        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = ErrorMessages.AtLeastOneAuthorRequired)]
        public List<int> AuthorIds { get; set; } = new List<int>();
    }
}
