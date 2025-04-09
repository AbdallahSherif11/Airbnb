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
    public class HouseAmenityRepository : IHouseAmenityRepository
    {
        private readonly AirbnbDbContext _context;
        public HouseAmenityRepository(AirbnbDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HouseAmenity>> GetAllForHouseAsync(int houseId)
        {
            return await _context.HouseAmenities
                .Where(ha => ha.HouseId == houseId)
                .ToListAsync();
        }

        public async Task<IEnumerable<HouseAmenity>> GetAllAsync()
        {
            return await _context.HouseAmenities.Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<HouseAmenity> GetAsync(int houseId, int amenityId)
        {
            return await _context.HouseAmenities.FindAsync(houseId, amenityId);

        }

        public async Task AddAsync(HouseAmenity houseAmenity)
        {
            await _context.HouseAmenities.AddAsync(houseAmenity);
        }

        public void Update(HouseAmenity houseAmenity)
        {
            _context.Entry(houseAmenity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task DeleteAsync(int houseId, int amenityId)
        {
            HouseAmenity ha = await GetAsync(houseId, amenityId);
            if (ha != null)
            {
                ha.IsDeleted = true;
            }
        }

    }
}
