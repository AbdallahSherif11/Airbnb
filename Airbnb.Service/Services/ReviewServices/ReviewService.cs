using Airbnb.Core.DTOs.ReviewDTOs;
using Airbnb.Core.Entities.Models;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Core.Services.Contract.Review.Contract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Service.Services.ReviewServices
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DetailedReadReviewDTO>> GetReviewsByHouseIdAsync(int houseId)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetReviewsByHouseIdAsync(houseId);

            return reviews.Select(r => new DetailedReadReviewDTO
            {
                ReviewId=r.ReviewId,
                BookingId = r.BookingId,
                ReviewerName = r.ApplicationUser.FirstName,
                Comment = r.Comment,
                Rating = r.Rating
            }).ToList();
        }

        public async Task AddReviewAsync(CreateReviewDTO dto)
        {
            var review = new Review
            {
                GuestId = dto.GuestId,
                BookingId = dto.BookingId,
                HouseId = dto.HouseId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.ReviewRepository.AddAsync(review);
            await _unitOfWork.CompleteSaveAsync();
        }

        public async Task UpdateReviewAsync(UpdateReviewDTO dto)
        {
            var review = await _unitOfWork.ReviewRepository.GetAsync(dto.ReviewId);
            if (review == null)
                throw new Exception("Review not found");

            review.Rating = dto.Rating;
            review.Comment = dto.Comment;

            _unitOfWork.ReviewRepository.Update(review);
            await _unitOfWork.CompleteSaveAsync();
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            var review = await _unitOfWork.ReviewRepository.GetAsync(reviewId);
            if (review == null)
                throw new Exception("Review not found");

            _unitOfWork.ReviewRepository.DeleteAsync(review.ReviewId);
            await _unitOfWork.CompleteSaveAsync();
        }
    }
}
