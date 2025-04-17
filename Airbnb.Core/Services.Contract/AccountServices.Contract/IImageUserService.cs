using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Services.Contract.AccountServices.Contract
{
    public interface IImageUserService
    {
        Task<string> SaveUserImageAsync(IFormFile imageFile, string userName);
        Task<bool> DeleteUserImageAsync(string imagePath);
    }
}
