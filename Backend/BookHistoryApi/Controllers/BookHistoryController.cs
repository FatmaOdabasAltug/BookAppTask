using BookHistoryApi.Messages;
using BookHistoryApi.Models.DTOs; // Import the constants namespace
using BookHistoryApi.Models.Entities;
using BookHistoryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookHistoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookHistoryController : BaseController
    {
        private readonly IBookHistoryService _bookHistoryService;

        public BookHistoryController(IBookHistoryService bookHistoryService)
        {
            _bookHistoryService = bookHistoryService;
        }

        // GET: api/bookHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookHistoryDto>>> GetBookHistories()
        {
            try
            {
                var bookHistories = await _bookHistoryService.GetAllBookHistoriesAsync();
                return Ok(bookHistories);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse(
                    ErrorMessages.UnexpectedError,
                    StatusCodes.Status500InternalServerError,
                    ex.Message
                );
            }
        }
    }
}
