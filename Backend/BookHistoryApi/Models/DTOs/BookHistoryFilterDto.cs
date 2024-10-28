using System.ComponentModel.DataAnnotations;
using BookHistoryApi.Messages;
using BookHistoryApi.Models.Constants;
using BookHistoryApi.Validation;

namespace BookHistoryApi.Models.DTOs
{
    public class BookHistoryFilterDto
    {
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.MustBeGreaterThanZeroPageNumber)]
        public int PageNumber { get; set; } = PageConstants.DefaultPageNumber;

        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.MustBeGreaterThanZeroPageSize)]
        public int PageSize { get; set; } = PageConstants.DefaultPageSize;

        [OrderByAttribute]
        public string OrderBy { get; set; } = SortConstants.Ascending; // Property to order by
        public string? Filter { get; set; } // Filter text to search for
        public string? GroupBy { get; set; } // Property to group by
    }
}
