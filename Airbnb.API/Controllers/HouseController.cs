using Airbnb.Core.DTOs.HouseAmenityDTO;
using Airbnb.Core.DTOs.HouseDTOs;
using Airbnb.Core.Entities.Models;
using Airbnb.Core.Services.Contract.HouseServices.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

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
        [Authorize]
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
        //[Consumes("multipart/form-data")]
        public async Task<ActionResult> AddHouse([FromForm] CreateHouseDTO createHouseDTO, [FromForm] List<CreateHouseAmenityDTO> createHouseAmenityDTO, [FromForm] List<IFormFile> images)
        {
            if (createHouseDTO == null)
            {
                return BadRequest("House data is invalid.");
            }
            await _houseService.AddHouseAsync(createHouseDTO, createHouseAmenityDTO, images);
            //return CreatedAtAction(nameof(GetHouseById), new { id = createHouseDTO.HouseId }, createHouseDTO);
            return Ok();
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



        #region Update House Region
        // PUT: api/house/5
        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateHouse(int id, [FromBody] House house)
        //{
        //    if (id != house.HouseId)
        //    {
        //        return BadRequest("House ID mismatch.");
        //    }
        //    await _houseService.UpdateHouseAsync(house);
        //    return NoContent();
        //}

        [HttpPut("title")]
        
        public async Task<ActionResult> UpdateHouseTitle(int Houseid, [FromBody] string Title)
        {
            if (Houseid != null && Title != null)
            {
                if (await _houseService.GetHouseByIdAsync(Houseid) == null)
                {
                    return NotFound();
                }
                await _houseService.UpdateHouseTitle(Houseid, Title);
                return Ok("House Updated");
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut("description")]
        public async Task<ActionResult> UpdateHouseDescription(int Houseid, [FromBody] string description)
        {
            if (Houseid != null && description != null)
            {
                if (await _houseService.GetHouseByIdAsync(Houseid) == null)
                {
                    return NotFound();
                }
                await _houseService.UpdateHouseDescription(Houseid, description);
                return Ok("House Updated");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("PricePerNight")]
        public async Task<ActionResult> UpdateHousePricePerNight(int Houseid, [FromBody] decimal PricePerNight)
        {
            if (Houseid != null && PricePerNight != null)
            {
                if (await _houseService.GetHouseByIdAsync(Houseid) == null)
                {
                    return NotFound();
                }
                await _houseService.UpdateHousePricePerNight(Houseid, PricePerNight);
                return Ok("House Updated");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("Location")]
        public async Task<ActionResult> UpdateHouseLocation(int Houseid, UpdateHouseLocationDTO updateHouseLocationDTO)
        {
            if (Houseid != null && updateHouseLocationDTO != null)
            {
                if (await _houseService.GetHouseByIdAsync(Houseid) == null)
                {
                    return NotFound();
                }
                await _houseService.UpdateHouseLocation(Houseid, updateHouseLocationDTO);
                return Ok("House Updated");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("Availability")]
        public async Task<ActionResult> UpdateHouseAvailability(int Houseid, UpdateHouseAvailabilityDTO updateHouseAvailabilityDTO)
        {
            if (Houseid != null && updateHouseAvailabilityDTO != null)
            {
                if (await _houseService.GetHouseByIdAsync(Houseid) == null)
                {
                    return NotFound();
                }
                await _houseService.UpdateHouseAvailability(Houseid, updateHouseAvailabilityDTO);
                return Ok("House Updated");
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut("Images")]
        public async Task<ActionResult> UpdateHouseImages(int Houseid, List<IFormFile> images)
        {
            if (Houseid != null && images != null)
            {
                if (await _houseService.GetHouseByIdAsync(Houseid) == null)
                {
                    return NotFound();
                }
                await _houseService.UpdateHouseImages(Houseid, images);
                return Ok("House Updated");
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion

        //[HttpGet("Reviewww")]
        //public async Task<IActionResult> getAllReviews()
        //{

        //}

    }
}
