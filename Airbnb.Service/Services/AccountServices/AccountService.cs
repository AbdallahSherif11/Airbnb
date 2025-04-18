using Airbnb.Core.DTOs.AccountDTOs;
using Airbnb.Core.Entities.Identity;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Core.Services.Contract.AccountServices.Contract;
using Airbnb.Core.Services.Contract.HouseServices.Contract;
using Airbnb.Core.Services.Contract.IdentityServices.Contract;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Service.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IImageUserService _imageUserService;
        private readonly IEmailService _emailService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration, IImageUserService imageUserService, IEmailService emailService, SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _imageUserService = imageUserService;
            _emailService = emailService;
            _signInManager = signInManager;
        }
        public async Task<bool> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            var user = _mapper.Map<ApplicationUser>(userRegisterDTO);

            var existingUser = await _userManager.FindByEmailAsync(userRegisterDTO.Email);
            if (existingUser != null)
                return false;

            IdentityResult r = _userManager.CreateAsync(user, userRegisterDTO.Password).Result;

            return r.Succeeded;
        }

        public async Task<string> LoginAsync(UserLoginDTO userLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userLoginDTO.Password))
            {
                return null; // Invalid credentials
            }

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Add a new method to update profile picture
        public async Task<bool> UpdateProfilePictureAsync(string userId, IFormFile profilePicture)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Delete old picture if it exists and isn't the default
            if (!string.IsNullOrEmpty(user.ProfilePictureUrl) &&
                !user.ProfilePictureUrl.Equals("/images/users/default/default-profile.png"))
            {
                await _imageUserService.DeleteUserImageAsync(user.ProfilePictureUrl);
            }

            // Save new picture
            user.ProfilePictureUrl = await _imageUserService.SaveUserImageAsync(
                profilePicture,
                user.UserName);

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<ReadUserDTO> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            return _mapper.Map<ReadUserDTO>(user);
        }

        public async Task<ReadUserDTO> GetUserByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            return _mapper.Map<ReadUserDTO>(user);
        }

        public async Task<ReadUserDTO> GetCurrentUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new KeyNotFoundException("User not found");

            return _mapper.Map<ReadUserDTO>(user);
        }

        public async Task<bool> UpdateUserProfileAsync(string userId, UpdateUserProfileDTO updateDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Update properties
            user.FirstName = updateDto.FirstName ?? user.FirstName;
            user.LastName = updateDto.LastName ?? user.LastName;
            user.Address = updateDto.Address ?? user.Address;
            user.DateOfBirth = updateDto.DateOfBirth ?? user.DateOfBirth;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordDTO changePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.ChangePasswordAsync(
                user,
                changePasswordDto.CurrentPassword,
                changePasswordDto.NewPassword);

            return result.Succeeded;
        }

        public async Task<bool> DeleteAccountAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Soft delete
            user.IsDeleted = true;
            var result = await _userManager.UpdateAsync(user);

            // Optional: Immediately sign out the user
            // await _signInManager.SignOutAsync();

            return result.Succeeded;
        }




        //Email

        public async Task<bool> RegisterAsync(UserRegisterDTO userRegisterDTO, bool requireEmailConfirmation = false)
        {
            var user = _mapper.Map<ApplicationUser>(userRegisterDTO);


            var result = await _userManager.CreateAsync(user, userRegisterDTO.Password);

            if (result.Succeeded && requireEmailConfirmation)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = $"{_configuration["AppBaseUrl"]}/api/account/confirm-email?userId={user.Id}&token={WebUtility.UrlEncode(token)}";

                var emailBody = $"Please confirm your email by <a href='{confirmationLink}'>clicking here</a>.";
                await _emailService.SendEmailAsync(user.Email, "Confirm your email", emailBody);
            }

            return result.Succeeded;
        }

        public async Task<string> LoginAsync(UserLoginDTO userLoginDTO, bool requireConfirmedEmail = false)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);
            if (user == null) return null;

            if (requireConfirmedEmail && !user.EmailConfirmed)
            {
                throw new UnauthorizedAccessException("Email not confirmed. Please check your email for confirmation link.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDTO.Password, false);
            if (!result.Succeeded) return null;

            return GenerateJwtToken(user);
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }



    }
}
