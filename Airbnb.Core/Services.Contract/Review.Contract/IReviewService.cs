using Airbnb.Core.DTOs.ReviewDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Services.Contract.Review.Contract
{
    public interface IReviewService
    {
        Task<List<DetailedReadReviewDTO>> GetReviewsByHouseIdAsync(int houseId);
        Task AddReviewAsync(CreateReviewDTO dto);
        Task UpdateReviewAsync(UpdateReviewDTO dto);
        Task DeleteReviewAsync(int reviewId);
    }
}
