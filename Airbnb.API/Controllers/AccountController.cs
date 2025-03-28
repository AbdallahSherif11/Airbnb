using Airbnb.Core.DTOs.AccountDTOs;
using Airbnb.Core.Services.Contract.HouseServices.Contract;
using Airbnb.Core.Services.Contract.IdentityServices.Contract;
using Airbnb.Service.Services.AccountServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            var r = _accountService.RegisterAsync(userRegisterDTO);
            if (await r)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
