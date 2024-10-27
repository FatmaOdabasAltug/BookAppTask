using BookHistoryApi.Models.DTOs;

namespace BookHistoryApi.Services.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDto> GetAuthors();
        void AddAuthor(AuthorDto author);
        IEnumerable<string> GetAuthorNamesByIds (IEnumerable<int> authorIds);
    }
}