using AutoMapper;
using BookHistoryApi.Data; // ApplicationDbContext
using BookHistoryApi.Models.DTOs;
using BookHistoryApi.Models.Entities; // Author entity
using BookHistoryApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore; // Entity Framework Core için gerekli

namespace BookHistoryApi.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthorService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // Method to add a new author
        public void AddAuthor(AuthorDto authorDto)
        {
            // Map DTO to entity
            var author = _mapper.Map<Author>(authorDto);

            // Add the new author to the database context
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges(); // Save changes to the database

            Console.WriteLine($"Adding Author: {author.Id}, Name: {author.Name}"); // Log the addition
        }

        // Method to get all authors
        public async Task<IEnumerable<AuthorDto>> GetAuthors()
        {
            var authors = await _dbContext.Authors.ToListAsync(); // Fetch authors from the database asynchronously
            return _mapper.Map<List<AuthorDto>>(authors); // Map to DTOs and return
        }

        // Method to get author names by IDs
        public async Task<IEnumerable<string>> GetAuthorNamesByIds(IEnumerable<int> authorIds)
        {
            return await _dbContext
                .Authors.Where(a => authorIds.Contains(a.Id))
                .Select(a => a.Name)
                .ToListAsync(); // Fetch names directly from the database asynchronously
        }

        public void DeleteAllAuthors()
        {
            var authors = _dbContext.Authors.ToList(); // Fetch all authors

            if (authors.Any())
            {
                _dbContext.Authors.RemoveRange(authors); // Remove all authors
                _dbContext.SaveChanges(); // Save changes to the database
                Console.WriteLine("All authors have been deleted.");
            }
            else
            {
                Console.WriteLine("No authors found to delete.");
            }
        }
    }
}
