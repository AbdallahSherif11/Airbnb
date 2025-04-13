using Airbnb.API.Errors;
using Airbnb.Core.DTOs.BookingDTOs;
using Airbnb.Core.Services.Contract.BookingServices.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }



        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDTO dto)
        {
            var guestId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (guestId == null)
                return Unauthorized(new ApiErrorResponse(401));

            var result = await _bookingService.CreateBookingAsync(dto, guestId);
            return Ok(result);
        }
    }
}
