using Airbnb.Core.Services.Contract.HouseServices.Contract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;


namespace Airbnb.Service.Services.HouseServices
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;
        private const string HouseImageFolder = "images/houses";

        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveHouseImageAsync(IFormFile imageFile, int houseId)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, HouseImageFolder, houseId.ToString());
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/{HouseImageFolder}/{houseId}/{uniqueFileName}";
        }

        public async Task<bool> DeleteHouseImageAsync(string imagePath)
        {
            var fullPath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }
    }
}
