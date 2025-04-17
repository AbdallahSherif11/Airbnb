using Airbnb.Core.DTOs.AccountDTOs;
using Airbnb.Core.Entities.Identity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Mapping.AccountMapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<ApplicationUser, ReadUserDTO>()
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.ProfilePictureUrl)
                        ? "https://localhost:7015/images/users/default/default-profile.png"
                        : $"https://localhost:7015{src.ProfilePictureUrl}"));
        }
    }
}
