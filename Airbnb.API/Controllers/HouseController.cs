using Airbnb.Core.DTOs.HouseDTOs;
using Airbnb.Core.Entities.Models;
using Airbnb.Core.Services.Contract.HouseServices.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseService _houseService;

        public HouseController(IHouseService houseService)
        {
            _houseService = houseService;
        }

        // GET: api/house
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadHouseDTO>>> GetAllHouses()
        {
            var houses = await _houseService.GetAllHousesAsync();
            if(!houses.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(houses);
            }
        }

        // GET: api/house/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadHouseDTO>> GetHouseById(int id)
        {
            var house = await _houseService.GetHouseByIdAsync(id);
            if (house == null)
            {
                return NotFound();
            }
            return Ok(house);
        }

        // GET: api/house/available
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<House>>> GetAvailableHouses()
        {
            var houses = await _houseService.GetAvailableHousesAsync();
            return Ok(houses);
        }

        // GET: api/house/city/{cityName}
        [HttpGet("city/{cityName}")]
        public async Task<ActionResult<IEnumerable<House>>> GetHousesByCity(string cityName)
        {
            var houses = await _houseService.GetHousesByCityAsync(cityName);
            return Ok(houses);
        }

        // GET: api/house/price?minPrice=100&maxPrice=500
        [HttpGet("price")]
        public async Task<ActionResult<IEnumerable<House>>> GetHousesByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var houses = await _houseService.GetHousesByPriceRangeAsync(minPrice, maxPrice);
            return Ok(houses);
        }

        // GET: api/house/search?keyword=beach
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<House>>> SearchHouses([FromQuery] string keyword)
        {
            var houses = await _houseService.SearchHousesAsync(keyword);
            return Ok(houses);
        }

        // POST: api/house
        [HttpPost]
        public async Task<ActionResult> AddHouse([FromBody] House house)
        {
            if (house == null)
            {
                return BadRequest("House data is invalid.");
            }
            await _houseService.AddHouseAsync(house);
            return CreatedAtAction(nameof(GetHouseById), new { id = house.HouseId }, house);
        }

        // PUT: api/house/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHouse(int id, [FromBody] House house)
        {
            if (id != house.HouseId)
            {
                return BadRequest("House ID mismatch.");
            }
            await _houseService.UpdateHouseAsync(house);
            return NoContent();
        }

        // DELETE: api/house/5 using id parameter only
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHouse(int id)
        {
            var house = await _houseService.GetHouseByIdAsync(id);
            if (house == null)
            {
                return NotFound();
            }
            await _houseService.DeleteHouseAsync(id);
            return NoContent();
        }
    }
}
