using Airbnb.Core.Services.Contract.AccountServices.Contract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Service.Services.AccountServices
{
    public class UserImageService : IImageUserService
    {
        private readonly IWebHostEnvironment _env;
        private const string UserImageFolder = "images/users";
        private const string DefaultImagePath = "images/users/default/default-profile.png";
        public UserImageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveUserImageAsync(IFormFile imageFile, string userName)
        {
            // Sanitize the username to create a valid folder name
            var sanitizedUserName = SanitizeUserName(userName);

            var uploadsFolder = Path.Combine(_env.WebRootPath, UserImageFolder, sanitizedUserName);
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/{UserImageFolder}/{sanitizedUserName}/{uniqueFileName}";
        }

        public async Task<bool> DeleteUserImageAsync(string imagePath)
        {
            var fullPath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }

        private string SanitizeUserName(string userName)
        {
            // Remove invalid characters from username to create a valid folder name
            var invalidChars = Path.GetInvalidFileNameChars();
            return string.Join("_", userName.Split(invalidChars));
        }
        public string GetDefaultProfilePictureUrl()
        {
            return $"/{DefaultImagePath}";
        }
    }
}
