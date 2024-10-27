using Microsoft.AspNetCore.Mvc;

namespace BookHistoryApi.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Creates a standardized error response.
        /// </summary>
        protected ActionResult CreateErrorResponse(string message, int statusCode,object errors = null)
        {
            return new ObjectResult(new
            {
                Success = false,
                Message = message,
                DetailedError = errors
            })
            {
                StatusCode = statusCode
            };
        }
    }
}