using Airbnb.Core.DTOs.AccountDTOs;
using Airbnb.Core.Entities.Identity;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Core.Services.Contract.HouseServices.Contract;
using Airbnb.Core.Services.Contract.IdentityServices.Contract;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Service.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<bool> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            var user = _mapper.Map<ApplicationUser>(userRegisterDTO);
            //var user = new ApplicationUser()
            //{
            //    UserName = userRegisterDTO.UserName,
            //    Email = userRegisterDTO.Email,
            //    FirstName = userRegisterDTO.FirstName,
            //    LastName = userRegisterDTO.LastName,
            //    DateOfBirth = UserRegisterDTO
            //}

            var existingUser = await _userManager.FindByEmailAsync(userRegisterDTO.Email);
            if (existingUser != null)
                return false;

            IdentityResult r = _userManager.CreateAsync(user, userRegisterDTO.Password).Result;

            return r.Succeeded;
            //if (!_userManager.FindByEmailAsync(userRegisterDTO.Email))
            //{

            //}
        }
    }
}
