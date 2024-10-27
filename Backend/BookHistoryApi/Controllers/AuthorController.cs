using BookHistoryApi.Messages;
using BookHistoryApi.Models.DTOs; // Import the constants namespace
using BookHistoryApi.Models.Entities;
using BookHistoryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookHistoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : BaseController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            try
            {
                var authors = await _authorService.GetAuthors();
                return Ok(authors);
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
