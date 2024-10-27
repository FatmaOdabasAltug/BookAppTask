using BookHistoryApi.Models.DTOs;

namespace BookHistoryApi.Services.Interfaces
{
    public interface IBookHistoryService
    {
        Task<IEnumerable<BookHistoryDto>> GetAllBookHistoriesAsync();
        Task AddBookHistoryAsync(BookHistoryDto bookHistoryDto);
    }
}