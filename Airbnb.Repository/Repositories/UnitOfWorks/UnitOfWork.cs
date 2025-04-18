﻿using Airbnb.Core.Repositories.Contract;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Core.Services.Contract.Review.Contract;
using Airbnb.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Repository.Repositories.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AirbnbDbContext _context;
        private IHouseRepository _houseRepository;
        private IAmenityRepository _amenityRepository;
        private IHouseAmenityRepository _houseAmenityRepository;
        private IReviewRepository _reviewRepository;
        private IBookingRepository _bookingRepository;
        private IMessageRepository _messageRepository;
        private IPaymentRepository _paymentRepository;
        private IWishListRepository _wishListRepository;

        public UnitOfWork(AirbnbDbContext context)
        {
            _context = context;
            //_houseRepository = new HouseRepository(context);
        }
        public IHouseRepository HouseRepository
        {
            get
            {
                if (_houseRepository == null)
                {
                    _houseRepository = new HouseRepository(_context);
                }
                return _houseRepository;
            }
        }

        public IAmenityRepository AmenityRepository
        {
            get
            {
                if (_amenityRepository == null)
                {
                    _amenityRepository = new AmenityRepository(_context);
                }
                return _amenityRepository;
            }
        }

        public IHouseAmenityRepository HouseAmenityRepository
        {
            get
            {
                if (_houseAmenityRepository == null)
                {
                    _houseAmenityRepository = new HouseAmenityRepository(_context);
                }
                return _houseAmenityRepository;
            }
        }

        public IReviewRepository ReviewRepository
        {
            get
            {
                if (_reviewRepository == null)
                {
                    _reviewRepository = new ReviewRepositroy(_context);
                }
                return _reviewRepository;
            }
        }

        public IBookingRepository BookingRepository
        {
            get
            {
                if (_bookingRepository == null)
                {
                    _bookingRepository = new BookingRepository(_context);
                }
                return _bookingRepository;
            }
        }
        public IMessageRepository MessageRepository
        {
            get
            {
                if (_messageRepository == null)
                {
                    _messageRepository = new MessageRepository(_context);
                }
                return _messageRepository;
            }
        }

        public IPaymentRepository PaymentRepository
        {
            get
            {
                if (_paymentRepository == null)
                {
                    _paymentRepository = new PaymentRepository(_context);
                }
                return _paymentRepository;
            }
        }

        public IWishListRepository WishListRepository
        {
            get
            {
                if (_wishListRepository == null)
                {
                    _wishListRepository = new WishListRepository(_context);
                }
                return _wishListRepository;
            }
        }


        public async Task<int> CompleteSaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
