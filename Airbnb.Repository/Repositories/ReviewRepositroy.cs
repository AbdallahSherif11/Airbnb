using Airbnb.Core.Entities.Models;
using Airbnb.Core.Repositories.Contract;
using Airbnb.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Repository.Repositories
{
    public class ReviewRepositroy : IReviewRepository
    {
        private readonly AirbnbDbContext _context;
        public ReviewRepositroy(AirbnbDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews.Where(r => r.IsDeleted == false).ToListAsync();
        }

        public async Task<Review> GetAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<IEnumerable<Review>> GetReviewsByHouseIdAsync(int houseId)
        {
            return await _context.Reviews
                .Include(r => r.ApplicationUser)
                .Where(r => r.HouseId == houseId && !r.IsDeleted)
                .ToListAsync();
        }

        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
        }

        public void Update(Review review)
        {
            _context.Entry(review).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            Review a = await GetAsync(id);
            if (a != null)
            {
                a.IsDeleted = true;
            }
        }
    }
}
