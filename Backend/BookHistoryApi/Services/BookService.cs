using AutoMapper;
using BookHistoryApi.Data;
using BookHistoryApi.Messages;
using BookHistoryApi.Models.DTOs;
using BookHistoryApi.Models.Entities;
using BookHistoryApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookHistoryApi.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IBookHistoryService _bookHistoryService;
        private readonly IAuthorService _authorService;

        public BookService(
            ApplicationDbContext dbContext,
            IMapper mapper,
            IBookHistoryService bookHistoryService,
            IAuthorService authorService
        )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _bookHistoryService = bookHistoryService;
            _authorService = authorService;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            // Fetch all books from the database
            var books = await _dbContext.Books.ToListAsync();

            // Use AutoMapper to convert the list of Book to a list of BookDto
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            // Book entity from the database
            var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            // Throw an exception if the book is not found
            if (book == null)
            {
                throw new KeyNotFoundException(string.Format(ErrorMessages.BookIdNotFound, id));
            }

            // Use AutoMapper to convert the Book entity to a BookDto
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> AddBookAsync(BookDto bookDto)
        {
            // Check if the title is unique
            if (!await IsTitleUniqueAsync(bookDto.Title))
            {
                throw new InvalidOperationException(
                    string.Format(ErrorMessages.BookTitleConflict, bookDto.Title)
                );
            }

            // Map the BookDto to a Book entity
            var book = _mapper.Map<Book>(bookDto);
            using var addTransaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // Add the book to the context
                book.CreatedTime = DateTime.UtcNow;
                await _dbContext.Books.AddAsync(book);
                await _dbContext.SaveChangesAsync();

                // Add bookHistory
                await PrepareBookHistoryAndAddAsync(
                    book.Id,
                    string.Format(InfoMessages.NewBookAdded, book.Title)
                );

                // Commit the transaction
                await addTransaction.CommitAsync();

                // Map the saved book back to a BookDto to return
                return _mapper.Map<BookDto>(book);
            }
            catch (Exception e)
            {
                // Rollback the transaction in case of an error
                await addTransaction.RollbackAsync();

                // Log the exception or handle it as needed
                throw new Exception("An error occurred while adding the book.", e);
            }
        }

        public async Task<BookDto> UpdateBookAsync(int bookId, BookDto bookDto)
        {
            // Check if the bookId matches with the bookDto.Id
            if (bookDto.Id != bookId)
            {
                throw new InvalidOperationException(ErrorMessages.BookIdMismatch);
            }

            // Check if the book exists
            if (bookId <= 0 || !await BookExistsAsync(bookId))
            {
                throw new KeyNotFoundException(
                    string.Format(ErrorMessages.BookIdNotFound, bookDto.Id)
                );
            }

            // Check if the title is unique for updates
            if (!await IsTitleUniqueAsync(bookDto.Title))
            {
                throw new InvalidOperationException(
                    string.Format(ErrorMessages.BookTitleConflict, bookDto.Title)
                );
            }

            // Retrieve the existing book
            var existingBook = await GetBookByIdAsync(bookId);
            using var updateTransaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // Update the entity and log changes if modified
                await UpdateAndAddBookHistoryAsync(existingBook, bookDto);

                // Save updates to the database
                await _dbContext.SaveChangesAsync();
                // Commit the transaction
                await updateTransaction.CommitAsync();

                // Return the updated DTO
                return _mapper.Map<BookDto>(existingBook);
            }
            catch (Exception e)
            {
                // Rollback the transaction in case of an error
                await updateTransaction.RollbackAsync();

                // Log the exception or handle it as needed
                throw new Exception("An error occurred while updating the book.", e);
            }
        }

        private async Task UpdateAndAddBookHistoryAsync(BookDto existingBook, BookDto bookDto)
        {
            // Add BookHistory and update title if changed
            if (existingBook.Title != bookDto.Title)
            {
                await PrepareBookHistoryAndAddAsync(
                    existingBook.Id,
                    string.Format(InfoMessages.TitleChanged, existingBook.Title, bookDto.Title)
                );
                existingBook.Title = bookDto.Title;
            }

            if (existingBook.ShortDescription != bookDto.ShortDescription)
            {
                await PrepareBookHistoryAndAddAsync(
                    existingBook.Id,
                    string.Format(
                        InfoMessages.DescriptionChanged,
                        existingBook.ShortDescription,
                        bookDto.ShortDescription
                    )
                );
                existingBook.ShortDescription = bookDto.ShortDescription;
            }

            if (existingBook.PublishDate != bookDto.PublishDate)
            {
                await PrepareBookHistoryAndAddAsync(
                    existingBook.Id,
                    string.Format(
                        InfoMessages.PublishDateChanged,
                        existingBook.PublishDate,
                        bookDto.PublishDate
                    )
                );
                existingBook.PublishDate = bookDto.PublishDate;
            }

            // Check for author changes
            var existingAuthorNames = _authorService.GetAuthorNamesByIds(existingBook.AuthorIds);
            var newAuthorNames = _authorService.GetAuthorNamesByIds(bookDto.AuthorIds);
            if (!existingAuthorNames.SequenceEqual(newAuthorNames))
            {
                var oldAuthors = string.Join(", ", existingAuthorNames);
                var newAuthors = string.Join(", ", newAuthorNames);
                await PrepareBookHistoryAndAddAsync(
                    existingBook.Id,
                    string.Format(InfoMessages.AuthorsChanged, oldAuthors, newAuthors)
                );

                existingBook.AuthorIds = bookDto.AuthorIds; // Update the authors
            }
            // Update the entity
            existingBook.UpdatedTime = DateTime.UtcNow;
            _dbContext.Books.Update(_mapper.Map<Book>(existingBook));
        }

        private async Task<bool> BookExistsAsync(int id)
        {
            return await _dbContext.Books.AnyAsync(b => b.Id == id);
        }

        private async Task<bool> IsTitleUniqueAsync(string title)
        {
            return await _dbContext.Books.AllAsync(b => b.Title != title);
        }

        private async Task PrepareBookHistoryAndAddAsync(int bookId, string description)
        {
            var bookHistoryDto = new BookHistoryDto
            {
                BookId = bookId,
                ChangeDescription = description,
            };

            await _bookHistoryService.AddBookHistoryAsync(bookHistoryDto);
        }
    }
}
