using Airbnb.API.Errors;
using Airbnb.Core.DTOs.AccountDTOs;
using Airbnb.Core.Services.Contract.HouseServices.Contract;
using Airbnb.Core.Services.Contract.IdentityServices.Contract;
using Airbnb.Service.Services.AccountServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            var r = _accountService.RegisterAsync(userRegisterDTO);
            if (await r)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiErrorResponse(400));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            var token = await _accountService.LoginAsync(userLoginDTO);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new ApiErrorResponse(401));
            }

            return Ok(new { Token = token });
        }

        [HttpPut("profile-picture")]
        [Authorize]
        public async Task<IActionResult> UpdateProfilePicture(IFormFile profilePicture)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _accountService.UpdateProfilePictureAsync(userId, profilePicture);

            if (!result)
                return BadRequest("Failed to update profile picture");

            return Ok("Profile picture updated successfully");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var userDto = await _accountService.GetUserByIdAsync(id);
                return Ok(userDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-username/{username}")]
        [Authorize]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            try
            {
                var userDto = await _accountService.GetUserByUsernameAsync(username);
                return Ok(userDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Update user profile (name, address, etc.)
        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileDTO updateDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _accountService.UpdateUserProfileAsync(userId, updateDto);
            return result ? Ok() : BadRequest("Failed to update profile");
        }

        // Change password
        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _accountService.ChangePasswordAsync(userId, dto);
            return result ? Ok() : BadRequest("Failed to change password");
        }

        // Soft delete account
        [HttpDelete("delete-account")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _accountService.DeleteAccountAsync(userId);
            return result ? Ok() : BadRequest("Failed to delete account");
        }
    }
}
