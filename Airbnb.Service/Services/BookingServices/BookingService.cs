﻿using Airbnb.Core.DTOs.BookingDTOs;
using Airbnb.Core.DTOs.PaymentDTOs;
using Airbnb.Core.Entities.Identity;
using Airbnb.Core.Entities.Models;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Core.Services.Contract.BookingServices.Contract;
using Airbnb.Core.Services.Contract.PaymentServices.Contract;
using Airbnb.Service.Services.PaymentServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Service.Services.BookingServices
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly StripeService _stripeService;

        //public BookingService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMapper mapper, StripeService stripeService)
        public BookingService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            //_stripeService = stripeService;
        }

        public async Task<ReadBookingForPaymentDTO> CreateBookingAsync(CreateBookingDTO dto, string userId)
        {
            // 1. Check if house exists
            var house = await _unitOfWork.HouseRepository.GetAsync(dto.HouseId);
            if (house == null || house.IsDeleted)
                return null;

            // 2. Check if user exists
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            // 3. Check availability (overlapping dates)
            bool isBooked = await _unitOfWork.BookingRepository.AnyAsync(b =>
                b.HouseId == dto.HouseId &&
                !b.IsDeleted &&
                (
                    (dto.CheckInDate >= b.CheckInDate && dto.CheckInDate < b.CheckOutDate) ||
                    (dto.CheckOutDate > b.CheckInDate && dto.CheckOutDate <= b.CheckOutDate) ||
                    (dto.CheckInDate <= b.CheckInDate && dto.CheckOutDate >= b.CheckOutDate)
                )
            );

            if (isBooked)
                return null;

            // 4. Calculate number of nights & total price
            int nights = dto.CheckOutDate.DayNumber - dto.CheckInDate.DayNumber;
            if (nights <= 0 || nights > house.MaxDays)
                return null;

            decimal totalPrice = house.PricePerNight * nights;

            // 5. Create new booking
            var booking = new Booking
            {
                GuestId = userId,
                HouseId = dto.HouseId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                TotalPrice = totalPrice,
                PaymentMethod = dto.PaymentMethod,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.BookingRepository.AddAsync(booking);
            await _unitOfWork.CompleteSaveAsync();

            // 6. Return DTO with guest name
            return new ReadBookingForPaymentDTO
            {
                BookingId = booking.BookingId,
                TotalPrice = totalPrice,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                GuestName = $"{user.FirstName} {user.LastName}"
            };
        }


        public async Task<IEnumerable<DetailedReadBookingDTO>> GetDetailedBookingsAsHostAsync(string hostId)
        {
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByHostIdAsync(hostId);
            return bookings.Select(b => new DetailedReadBookingDTO
            {
                BookingId = b.BookingId,
                GuestName = $"{b.ApplicationUser.FirstName} {b.ApplicationUser.LastName}",
                GuestEmail = b.ApplicationUser.Email,
                HouseTitle = b.House.Title,
                HouseAddress = $"{b.House.Street}, {b.House.City}, {b.House.Country}",
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate,
                TotalPrice = b.TotalPrice,
                PaymentMethod = b.PaymentMethod,
                CreatedAt = b.CreatedAt
            });
        }

        public async Task<IEnumerable<DetailedReadBookingDTO>> GetDetailedBookingsAsGuestAsync(string guestId)
        {
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByGuestIdAsync(guestId);
            return bookings.Select(b => new DetailedReadBookingDTO
            {
                BookingId = b.BookingId,
                GuestName = $"{b.ApplicationUser.FirstName} {b.ApplicationUser.LastName}",
                GuestEmail = b.ApplicationUser.Email,
                HouseTitle = b.House.Title,
                GuestId =b.GuestId,
                HouseId = b.HouseId,
                HouseAddress = $"{b.House.Street}, {b.House.City}, {b.House.Country}",
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate,
                TotalPrice = b.TotalPrice,
                PaymentMethod = b.PaymentMethod,
                CreatedAt = b.CreatedAt
            });
        }
    }
}
