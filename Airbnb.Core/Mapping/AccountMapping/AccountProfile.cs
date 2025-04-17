using Airbnb.Core.DTOs.AccountDTOs;
using Airbnb.Core.DTOs.HouseDTOs;
using Airbnb.Core.Entities.Identity;
using Airbnb.Core.Entities.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Mapping.AccountMapping
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            //CreateMap<ApplicationUser, UserRegisterDTO>().AfterMap((src, dest) =>
            //{

            //}).ReverseMap();

            CreateMap<UserRegisterDTO, ApplicationUser>().ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                                                         .AfterMap((src, dest) =>
                                                         {

                                                         })
                                                         .ReverseMap();
                                                         }
    }
}
