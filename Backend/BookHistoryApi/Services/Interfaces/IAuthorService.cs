using BookHistoryApi.Models.DTOs;

namespace BookHistoryApi.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAuthors();
        void AddAuthor(AuthorDto author);
        Task<IEnumerable<string>> GetAuthorNamesByIds(IEnumerable<int> authorIds);
        void DeleteAllAuthors();
    }
}
