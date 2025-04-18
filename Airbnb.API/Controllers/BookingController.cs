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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingController(IBookingService bookingService, IHttpContextAccessor httpContextAccessor)
        {
            _bookingService = bookingService;
            _httpContextAccessor = httpContextAccessor;
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


        [HttpGet("AsHost")]
        public async Task<IActionResult> GetDetailedBookingsAsHost()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var bookings = await _bookingService.GetDetailedBookingsAsHostAsync(userId);
            return Ok(bookings);
        }

        [HttpGet("AsGuest")]
        public async Task<IActionResult> GetDetailedBookingsAsGuest()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var bookings = await _bookingService.GetDetailedBookingsAsGuestAsync(userId);
            return Ok(bookings);
        }
    }
}
