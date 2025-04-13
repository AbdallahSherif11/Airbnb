using Airbnb.API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.API.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        //[HttpGet, Route("{code}")]
        public IActionResult HandleError(int code)
        {
            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound,"Not Found Endpoint !!"));
        }
    }
}
