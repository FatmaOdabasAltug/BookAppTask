using Microsoft.AspNetCore.Mvc;

namespace BookHistoryApi.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult CreateErrorResponse(
            string message,
            int statusCode,
            object errors = null
        )
        {
            return new ObjectResult(
                new
                {
                    Success = false,
                    Message = message,
                    DetailedError = errors,
                }
            )
            {
                StatusCode = statusCode,
            };
        }
    }
}
