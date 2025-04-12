using Airbnb.Core.DTOs.BookingDTOs;
using Airbnb.Core.DTOs.HouseAmenityDTO;
using Airbnb.Core.DTOs.HouseDTOs;
using Airbnb.Core.DTOs.ReviewDTOs;
using Airbnb.Core.Entities.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Mapping.HouseMapping
{
    public class HouseProfile : Profile
    {
        public HouseProfile()
        {
            CreateMap<House, ReadHouseDTO>()
            .ForMember(dest => dest.Images, opt => opt.Ignore())
            .ForMember(dest => dest.HostName, opt => opt.Ignore())
            .ForMember(dest => dest.Amenities, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Bookings, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Images = src.Images?.Where(i=> i.IsDeleted == false).Select(i => $"https://localhost:7015/{i.Url}").ToList() ?? new List<string>();
                dest.HostName = src.ApplicationUser?.FirstName ?? string.Empty;
                dest.Amenities = src.HouseAmenities?.Where(a=> a.IsDeleted == false).Select(ha => ha.Amenity.Name).ToList() ?? new List<string>();

                dest.Reviews = src.Reviews?.Where(r=> r.IsDeleted== false).Select(r => new ReadReviewDTO
                {
                    ReviewerName = r.ApplicationUser?.FirstName ?? "",
                    Comment = r.Comment,
                    Rating = r.Rating
                }).ToList() ?? new List<ReadReviewDTO>();

                dest.Bookings = src.Bookings?.Select(b => new ReadBookingDTO
                {
                    CheckIn = b.CheckInDate,
                    CheckOut = b.CheckOutDate,
                    GuestName = b.ApplicationUser?.FirstName ?? ""
                }).ToList() ?? new List<ReadBookingDTO>();
            });


            CreateMap<House, CreateHouseDTO>().AfterMap((src, dest) =>
            {

            }).ReverseMap();


            CreateMap<House, UpdateHouseDTO>().AfterMap((src, dest) =>
            {

            }).ReverseMap();
        }
    }
}
