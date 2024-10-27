using Microsoft.AspNetCore.Mvc;
using BookHistoryApi.Models.Entities;
using BookHistoryApi.Services.Interfaces;
using BookHistoryApi.Messages;
using BookHistoryApi.Models.DTOs; // Import the constants namespace

namespace BookHistoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse(ErrorMessages.UnexpectedError, StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/book
        [HttpPost]
        public async Task<ActionResult<BookDto>> AddBook([FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return CreateErrorResponse(ErrorMessages.InvalidBookData, StatusCodes.Status400BadRequest, ModelState);
            }

            try
            {
                var createdBook = await _bookService.AddBookAsync(bookDto);
                return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
            }
            catch (InvalidOperationException ex)
            {
                return CreateErrorResponse(ErrorMessages.ConflictCreatingBook, StatusCodes.Status409Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse(ErrorMessages.UnexpectedError, StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/book/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                return Ok(book);
            }
            catch (KeyNotFoundException ex)
            {
                return CreateErrorResponse(ErrorMessages.BookIdNotFound, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse(ErrorMessages.UnexpectedError, StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT: api/book/{bookId}
        [HttpPut("{bookId}")]
        public async Task<ActionResult<BookDto>> UpdateBook(int bookId,[FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return CreateErrorResponse(ErrorMessages.InvalidBookData, StatusCodes.Status400BadRequest, ModelState);
            }

            try
            {
                var updatedBook = await _bookService.UpdateBookAsync(bookId,bookDto);
                return Ok(updatedBook);
            }
            catch (KeyNotFoundException ex)
            {
                return CreateErrorResponse(ErrorMessages.BookIdNotFound, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return CreateErrorResponse(ErrorMessages.ConflictUpdatingBook, StatusCodes.Status409Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse(ErrorMessages.UnexpectedError, StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
