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
    public class HouseRepository : IHouseRepository
    {
        private readonly AirbnbDbContext _context;
        public HouseRepository(AirbnbDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<House>> GetAllAsync()
        {
            return await _context.Houses.Where(p=> p.IsDeleted == false).ToListAsync();
        }

        public async Task<House> GetAsync(int id)
        {
            return await _context.Houses.FindAsync(id);
        }

        public async Task AddAsync(House house)
        {
            await _context.Houses.AddAsync(house);
        }
        public void Update(House house)
        {
            _context.Entry(house).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task Delete(House house)
        {
            House h = await GetAsync(house.HouseId);
            h.IsDeleted = true;
        }

        public async Task<IEnumerable<House>> GetAvailableHousesAsync()
        {
            return await _context.Houses.Where(H=> H.IsDeleted == false && H.IsAvailable == true).ToListAsync();
        }

        public async Task<IEnumerable<House>> GetHousesByCityAsync(string city)
        {
            return await _context.Houses.Where(H => H.IsDeleted == false && H.City == city).ToListAsync();
        }

        public async Task<IEnumerable<House>> GetHousesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.Houses
                .Where(h => h.PricePerNight >= minPrice && h.PricePerNight <= maxPrice && h.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<House>> SearchHousesAsync(string keyword)
        {
            return await _context.Houses
                            .Where(h => (h.Title.Contains(keyword) ||
                                    h.Description.Contains(keyword) ||
                                    h.Country.Contains(keyword) ||
                                    h.City.Contains(keyword) ||
                                    h.Street.Contains(keyword) ||
                                    h.View.Contains(keyword)) && h.IsDeleted == false
                                    )
                            .ToListAsync();
        }
    }
}
