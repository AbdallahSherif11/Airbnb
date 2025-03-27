using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Services.Contract.HouseServices.Contract
{
    public interface IImageService
    {
        Task<string> SaveHouseImageAsync(IFormFile imageFile, int houseId);
        Task<bool> DeleteHouseImageAsync(string imagePath);
    }
}
