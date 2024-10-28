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

        // GET: api/filterBookHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookHistoryDto>>> FilterBookHistories(
            [FromQuery] BookHistoryFilterDto bookHistoryFilterDto
        )
        {
            try
            {
                var bookHistories = await _bookHistoryService.FilterBookHistoriesAsync(
                    bookHistoryFilterDto
                );
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
