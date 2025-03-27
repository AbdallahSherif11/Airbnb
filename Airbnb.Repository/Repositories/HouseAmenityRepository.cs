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

        public async Task<IEnumerable<HouseAmenity>> GetAllAsync()
        {
            return await _context.HouseAmenities.Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<HouseAmenity> GetAsync(int id)
        {
            return await _context.HouseAmenities.FindAsync(id);
        }

        public async Task AddAsync(HouseAmenity houseAmenity)
        {
            await _context.HouseAmenities.AddAsync(houseAmenity);
        }

        public void Update(HouseAmenity houseAmenity)
        {
            _context.Entry(houseAmenity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            HouseAmenity ha = await GetAsync(id);
            if (ha != null)
            {
                ha.IsDeleted = true;
            }
        }
                
    }
}
