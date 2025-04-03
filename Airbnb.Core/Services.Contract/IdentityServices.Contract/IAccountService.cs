using Airbnb.Core.DTOs.AccountDTOs;
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
    }
}
