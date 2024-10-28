using System.ComponentModel.DataAnnotations;
using BookHistoryApi.Messages;

namespace BookHistoryApi.Models.DTOs
{
    public class BookHistoryDto : AuditableDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.BookIdRequired)]
        public required int BookId { get; set; }

        [Required(ErrorMessage = ErrorMessages.ChangeDescriptionRequired)]
        public required string ChangeDescription { get; set; }
        public required string OldValue { get; set; }
        public required string NewValue { get; set; }
        public required string ChangedProperty { get; set; }
    }
}
