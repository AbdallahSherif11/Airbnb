using Airbnb.Core.DTOs.HouseDTOs;
using Airbnb.Core.Services.Contract.HouseServices.Contract;
using Airbnb.Core.Services.Contract.WishListService.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Airbnb.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService _wishListService;
        private readonly IHouseService _houseService;

        public WishListController(IWishListService wishListService,IHouseService houseService)
        {
            _wishListService = wishListService;
            _houseService = houseService;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpPost("add/{houseId}")]
        public async Task<IActionResult> AddToWishList(int houseId)
        {
            var userId = GetUserId();
            await _wishListService.AddToWishListAsync(userId, houseId);
            return Ok();
        }
        [HttpDelete("remove/{houseId}")]
        public async Task<IActionResult> RemoveFromWishList(int houseId)
        {
            var userId = GetUserId(); // Get logged-in user ID
            await _wishListService.RemoveFromWishListAsync(userId, houseId); 
            return Ok();
        }

        // GET: api/wishlist/is-favorite/{houseId}
        // Checks if a house is already in the user's wishlist (is favorited)
        [HttpGet("is-favorite/{houseId}")]
        public async Task<IActionResult> IsFavorite(int houseId)
        {
            var userId = GetUserId(); // Get logged-in user ID
            var isFav = await _wishListService.IsFavoriteAsync(userId, houseId); // Check favorite
            return Ok(isFav); 
        }

        [HttpGet("my-wishlist")]
        public async Task<IActionResult> GetMyWishList()
        {
            var userId = GetUserId(); 
            var wishlist = await _wishListService.GetUserWishListAsync(userId);
            var houseIds = wishlist.Select(w => w.HouseId).ToList(); // Get list of house IDs
            List<ReadHouseDTO> ReadHouses = new List<ReadHouseDTO>();
            foreach (var x in houseIds)
            {
                ReadHouses.Add(await _houseService.GetHouseByIdAsync(x)); // Get house details
            }
            return Ok(ReadHouses);
        }
    }
}
