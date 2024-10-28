using System.Threading.Tasks;
using AutoMapper;
using BookHistoryApi.Data;
using BookHistoryApi.Models.Constants;
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

        public async Task<IEnumerable<BookHistoryDto>> FilterBookHistoriesAsync(
            BookHistoryFilterDto bookHistoryFilterDto
        )
        {
            var query = _dbContext.BookHistories.AsQueryable();

            // Apply filtering
            if (!string.IsNullOrEmpty(bookHistoryFilterDto.GroupBy))
            {
                var isGroupByNumeric = IsNumeric(bookHistoryFilterDto.GroupBy);
                var groupBy = isGroupByNumeric
                    ? bookHistoryFilterDto.GroupBy
                    : bookHistoryFilterDto.GroupBy.Trim().ToUpper();
                query = query.Where(b =>
                    (isGroupByNumeric ? b.ChangedProperty : b.ChangedProperty.ToUpper()) == groupBy
                );
            }

            if (!string.IsNullOrEmpty(bookHistoryFilterDto.Filter))
            {
                bool isFilterNumeric = IsNumeric(bookHistoryFilterDto.Filter);
                var filter = isFilterNumeric
                    ? bookHistoryFilterDto.Filter
                    : bookHistoryFilterDto.Filter.Trim().ToUpper();

                query = query.Where(b =>
                    (isFilterNumeric ? b.NewValue : b.NewValue.ToUpper()).Contains(filter)
                    || (isFilterNumeric ? b.OldValue : b.OldValue.ToUpper()).Contains(filter)
                    || (
                        isFilterNumeric ? b.ChangeDescription : b.ChangeDescription.ToUpper()
                    ).Contains(filter)
                );
            }

            // Apply ordering
            query =
                bookHistoryFilterDto.OrderBy.ToUpper() == SortConstants.Ascending
                    ? query.OrderBy(b => b.CreatedTime)
                    : query.OrderByDescending(b => b.CreatedTime);

            // Apply pagination
            query = query
                .Skip((bookHistoryFilterDto.PageNumber - 1) * bookHistoryFilterDto.PageSize)
                .Take(bookHistoryFilterDto.PageSize);

            var pagedBookHistory = await query.ToListAsync();

            return _mapper.Map<IEnumerable<BookHistoryDto>>(pagedBookHistory);
        }

        private bool IsNumeric(string value)
        {
            return double.TryParse(value, out _);
        }
    }
}
