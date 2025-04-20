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
        [HttpGet]
        public async Task<IActionResult> GetUserById()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new ApiErrorResponse(401, "User is not authorized."));
            try
            {
                var userDto = await _accountService.GetUserByIdAsync(userId);
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








        [HttpPost("register-with-confirmation")]
        public async Task<IActionResult> RegisterWithConfirmation(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var result = await _accountService.RegisterAsync(userRegisterDTO, true);
                return result
                    ? Ok(new { Message = "Registration successful. Please check your email for confirmation link." })
                    : BadRequest(new ApiErrorResponse(400, "Registration failed"));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ApiErrorResponse(409, ex.Message)); // Return 409 Conflict for duplicate entries
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }

        [HttpPost("login-with-confirmation")]
        public async Task<IActionResult> LoginWithConfirmation(UserLoginDTO userLoginDTO)
        {
            try
            {
                var token = await _accountService.LoginAsync(userLoginDTO, true);
                return string.IsNullOrEmpty(token) ?
                    Unauthorized(new ApiErrorResponse(401)) :
                    Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiErrorResponse(401, ex.Message));
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var result = await _accountService.ConfirmEmailAsync(userId, token);
            return result ?
                Ok(new { Message = "Email confirmed successfully. You can now login." }) :
                BadRequest(new ApiErrorResponse(400, "Invalid or expired confirmation link"));
        }
    }
}
