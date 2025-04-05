using Airbnb.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Repositories.Contract
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review> GetAsync(int id);
        Task<IEnumerable<Review>> GetReviewsByHouseIdAsync(int houseId);
        Task AddAsync(Review review);
        void Update(Review review);
        Task DeleteAsync(int id);
    }
}
