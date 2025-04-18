using Airbnb.Core.DTOs.AccountDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Services.Contract.IdentityServices.Contract
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(UserRegisterDTO userRegisterDTO);
        Task<string> LoginAsync(UserLoginDTO userLoginDTO);
        Task<bool> UpdateProfilePictureAsync(string userId, IFormFile profilePicture);
        Task<ReadUserDTO> GetUserByIdAsync(string userId);
        Task<ReadUserDTO> GetUserByUsernameAsync(string username);

        Task<bool> UpdateUserProfileAsync(string userId, UpdateUserProfileDTO updateDto);
        Task<bool> ChangePasswordAsync(string userId, ChangePasswordDTO changePasswordDto);
        Task<bool> DeleteAccountAsync(string userId);
        Task<ReadUserDTO> GetCurrentUserAsync(string userId);


        Task<bool> RegisterAsync(UserRegisterDTO userRegisterDTO, bool requireEmailConfirmation = false);
        Task<string> LoginAsync(UserLoginDTO userLoginDTO, bool requireConfirmedEmail = false);
        Task<bool> ConfirmEmailAsync(string userId, string token);

    }
}
