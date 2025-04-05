using Airbnb.Core.DTOs.ReviewDTOs;
using Airbnb.Core.Services.Contract.Review.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: api/Review/house/5
        [HttpGet("house/{houseId}")]
        public async Task<IActionResult> GetReviewsByHouseId(int houseId)
        {
            var reviews = await _reviewService.GetReviewsByHouseIdAsync(houseId);
            return Ok(reviews);
        }

        // POST: api/Review
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] CreateReviewDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _reviewService.AddReviewAsync(dto);
            return Ok(new { message = "Review added successfully" });
        }

        // PUT: api/Review
        [HttpPut]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _reviewService.UpdateReviewAsync(dto);
            return Ok(new { message = "Review updated successfully" });
        }

        // DELETE: api/Review/5
        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            await _reviewService.DeleteReviewAsync(reviewId);
            return Ok(new { message = "Review deleted successfully" });
        }
    }
}
