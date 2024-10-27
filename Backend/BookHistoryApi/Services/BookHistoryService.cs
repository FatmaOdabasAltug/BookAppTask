using System.Threading.Tasks;
using AutoMapper;
using BookHistoryApi.Data;
using BookHistoryApi.Models.DTOs;
using BookHistoryApi.Models.Entities;
using BookHistoryApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookHistoryApi.Services
{
    public class BookHistoryService : IBookHistoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public BookHistoryService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddBookHistoryAsync(BookHistoryDto bookHistoryDto)
        {
            // Map the DTO to the entity
            var bookHistory = _mapper.Map<BookHistory>(bookHistoryDto);

            // Add the new entity to the database
            bookHistory.CreatedTime = DateTime.UtcNow;
            await _dbContext.BookHistories.AddAsync(bookHistory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookHistoryDto>> GetAllBookHistoriesAsync()
        {
            // Retrieve all BookHistory records from the database
            var bookHistories = await _dbContext.BookHistories.ToListAsync();

            // Map BookHistory records to BookHistoryDto
            var bookHistoryDtos = _mapper.Map<IEnumerable<BookHistoryDto>>(bookHistories);

            // Return the list of DTOs
            return bookHistoryDtos;
        }
    }
}
