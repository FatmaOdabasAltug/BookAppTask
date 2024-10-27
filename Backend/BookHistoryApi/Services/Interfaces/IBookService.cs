using BookHistoryApi.Models.DTOs;

namespace BookHistoryApi.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookDto> GetBookByIdAsync(int id);
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> AddBookAsync(BookDto bookDto);
        Task<BookDto> UpdateBookAsync(int bookId ,BookDto bookDto);
    }
}