using BookHistoryApi.Models.DTOs;

namespace BookHistoryApi.Services.Interfaces
{
    public interface IBookHistoryService
    {
        Task<IEnumerable<BookHistoryDto>> FilterBookHistoriesAsync(
            BookHistoryFilterDto bookHistoryFilterDto
        );
        Task AddBookHistoryAsync(BookHistoryDto bookHistoryDto);
    }
}
