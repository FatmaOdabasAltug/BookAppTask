using System.ComponentModel.DataAnnotations;
using BookHistoryApi.Messages;
using BookHistoryApi.Models.Validation;

namespace BookHistoryApi.Models.DTOs
{
    public class AuthorDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
