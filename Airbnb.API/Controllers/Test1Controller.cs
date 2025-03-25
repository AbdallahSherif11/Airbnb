using Airbnb.Core.Entities.Identity;
using Airbnb.Core.Entities.Models;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Repository.Data.Contexts;
using Airbnb.Repository.Repositories.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test1Controller : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public Test1Controller(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("add-test-user")]
        public async Task<IActionResult> AddTestUser()
        {
            try
            {
                var testUser = new ApplicationUser
                {
                    UserName = "testuser@example.com",
                    Email = "testuser@example.com",
                    FirstName = "Test",
                    LastName = "User",
                    DateOfBirth = new DateOnly(1990, 1, 1),
                    Address = "123 Test Street, Test City",
                    ProfilePictureUrl = "https://example.com/profile.jpg",
                    NationalId = "12345678901234",
                    IsAgreed = true,
                    IsDeleted = false,
                    EmailConfirmed = true // Important if you require email confirmation
                };

                // Create the user with a password
                var result = await _userManager.CreateAsync(testUser, "Test@1234");

                if (result.Succeeded)
                {
                    return Ok(new
                    {
                        Message = "Test user created successfully",
                        UserId = testUser.Id
                    });
                }

                return BadRequest(new
                {
                    Message = "Failed to create test user",
                    Errors = result.Errors.Select(e => e.Description)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("add-test-house/{hostId}")]
        public async Task<IActionResult> AddTestHouse(string hostId)
        {
            try
            {
                // Verify the host exists
                var host = await _userManager.FindByIdAsync(hostId);
                if (host == null)
                {
                    return NotFound(new { Message = "Host user not found" });
                }

                // Create the test house
                var house = CreateTestHouse(hostId);

                // Add using repository
                await _unitOfWork.HouseRepository.AddAsync(house);

                // Save changes
                var result = await _unitOfWork.CompleteSaveAsync();

                if (result > 0)
                {
                    return Ok(new
                    {
                        Message = "Test house created successfully",
                        HouseId = house.HouseId,
                        HostId = house.HostId
                    });
                }

                return BadRequest(new { Message = "Failed to save test house" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred while creating test house",
                    Error = ex.Message
                });
            }
        }

        private House CreateTestHouse(string hostId)
        {
            return new House
            {
                HostId = hostId,
                Title = "Beautiful Test Villa",
                Description = "This is a dummy house created for testing purposes",
                PricePerNight = 99.99m,
                Country = "Testland",
                City = "Testville",
                Street = "123 Test Street",
                Latitude = 35.6895m,
                Longitude = 139.6917m,
                IsAvailable = true,
                MaxDays = 30,
                MaxGuests = 4,
                View = "Mountain view",
                NumberOfRooms = 2,
                NumberOfBeds = 3,
                IsDeleted = false,
                Images = new List<Image>
                {
                    new Image { Url = "https://example.com/house1.jpg" },
                    new Image { Url = "https://example.com/house2.jpg" }
                },
                HouseAmenities = new List<HouseAmenity>
                {
                    //new HouseAmenity { AmenityId = 1 }, // WiFi
                    //new HouseAmenity { AmenityId = 2 }  // Pool
                }
            };
        }
    }
}
