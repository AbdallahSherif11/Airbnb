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
    public class AmenityRepository : IAmenityRepository
    {
        private readonly AirbnbDbContext _context;
        public AmenityRepository(AirbnbDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Amenity>> GetAllAsync()
        {
            return await _context.Amenities.Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<Amenity> GetAsync(int id)
        {
            return await _context.Amenities.FindAsync(id);
        }

        public async Task AddAsync(Amenity amenity)
        {
            await _context.Amenities.AddAsync(amenity);
        }

        public void Update(Amenity amenity)
        {
            _context.Entry(amenity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            Amenity a = await GetAsync(id);
            if (a != null)
            {
                a.IsDeleted = true;
            }
        }       
        
    }
}
