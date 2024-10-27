using BookHistoryApi.Models.DTOs;
using BookHistoryApi.Services.Interfaces;

namespace BookHistoryApi.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly List<AuthorDto> _authors = new();

        // Method to add a new author
        public void AddAuthor(AuthorDto author)
        {
            // Add the new author to the list
            _authors.Add(author); 
            Console.WriteLine($"Adding Author: {author.Id}, Name: {author.Name}"); // Log the addition
        }

        // Method to get all authors
        public IEnumerable<AuthorDto> GetAuthors()
        {
            return _authors; // Return the list of authors
        }

        // Method to get an GetAuthorNamesByIds
        public IEnumerable<string> GetAuthorNamesByIds(IEnumerable<int> authorIds)
        {
            // Get the names of the authors with the specified IDs
            return _authors.Where(a => authorIds.Contains(a.Id)).Select(a => a.Name);
        }
    }
}