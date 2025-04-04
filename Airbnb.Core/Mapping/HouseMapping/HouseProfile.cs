﻿using Airbnb.Core.DTOs.BookingDTOs;
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
            CreateMap<House, ReadHouseDTO>().AfterMap((src, dest) =>
            {
                dest.Images = src.Images.Where(I=> I.IsDeleted == false).Select(I => I.Url).ToList();
                dest.HostName = src.ApplicationUser.FirstName;
                dest.Amenities = src.HouseAmenities.Select(HA => HA.Amenity.Name).ToList();

                dest.Reviews = src.Reviews.Select(R => new ReadReviewDTO
                {
                    ReviewerName = R.ApplicationUser.FirstName,
                    Comment = R.Comment,
                    Rating = R.Rating
                }).ToList();

                dest.Bookings = src.Bookings.Select(B => new ReadBookingDTO
                {
                    CheckIn = B.CheckInDate,
                    CheckOut = B.CheckOutDate,
                    GuestName = B.ApplicationUser.FirstName
                }).ToList();

            }).ReverseMap();


            CreateMap<House, CreateHouseDTO>().AfterMap((src, dest) =>
            {

            }).ReverseMap();

            //CreateMap<House, CreateHouseDTO>().AfterMap((src, dest) =>
            //{
            //    dest.CHouseAmenities = src.HouseAmenities.Select(ha => new CreateHouseAmenityDTO
            //    {
            //        AmenityId = ha.AmenityId
            //    }).ToList();
            //}).ReverseMap().AfterMap((src, dest) =>
            //{
            //    dest.HouseAmenities = src.CHouseAmenities.Select(ha => new HouseAmenity
            //    {
            //        AmenityId = ha.AmenityId,
            //        HouseId = dest.HouseId
            //    }).ToList();
            //});


            CreateMap<House, UpdateHouseDTO>().AfterMap((src, dest) =>
            {

            }).ReverseMap();
        }
    }
}
