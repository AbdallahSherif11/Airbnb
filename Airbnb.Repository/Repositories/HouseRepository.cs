using Airbnb.Core.Entities.Models;
using Airbnb.Core.Repositories.Contract;
using Airbnb.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<House>> GetHousesByConditionAsync(Expression<Func<House, bool>> predicate)
        {
            return await _context.Houses.Where(predicate).ToListAsync();
        }

        public async Task<House> GetAsync(int id)
        {
            //return await _context.Houses.FindAsync(id);
            return await _context.Houses.FirstOrDefaultAsync(H => H.HouseId == id && H.IsDeleted == false);
        }

        public async Task AddAsync(House house)
        {
            await _context.Houses.AddAsync(house);
        }
        public void Update(House house)
        {
            _context.Entry(house).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            House h = await GetAsync(id);
            if (h != null)
            {
                h.IsDeleted = true;
                //foreach(var x in h.HouseAmenities)
                //{
                //    x.IsDeleted = true;
                //}
            }
        }

        public async Task<IEnumerable<House>> GetAvailableHousesAsync()
        {
            return await _context.Houses.Where(H=> H.IsDeleted == false && H.IsAvailable == true).ToListAsync();
        }

        public async Task<IEnumerable<House>> GetHousesByCityAsync(string city)
        {
            return await _context.Houses.Where(H => H.IsDeleted == false && H.City == city).ToListAsync();
        }

        public async Task<IEnumerable<House>> GetHousesByViewAsync(string view)
        {
            return await _context.Houses.Where(H => H.IsDeleted == false && H.HouseView == view).ToListAsync();
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
                                    h.HouseView.Contains(keyword)) && h.IsDeleted == false
                                    )
                            .ToListAsync();
        }





        public async Task AddImageAsync(int houseId, Image image)
        {
            var house = await _context.Houses.Include(h => h.Images)
                            .FirstOrDefaultAsync(h => h.HouseId == houseId);
            if (house != null)
            {
                house.Images ??= new List<Image>();
                house.Images.Add(image);
            }
        }

        public async Task<Image> GetImageAsync(int imageId)
        {
            return await _context.Images.FindAsync(imageId);
        }

        public async Task DeleteImageAsync(int imageId)
        {
            var image = await GetImageAsync(imageId);
            if (image != null)
            {
                _context.Images.Remove(image);
            }
        }


        public IQueryable<House> GetQueryable()
        {
            return _context.Houses.Where(h => !h.IsDeleted);
        }


        public IQueryable<House> GetQueryableWithIncludes()
        {
            return _context.Houses
                .Where(h => !h.IsDeleted)
                .Include(h => h.Images)
                .Include(h => h.HouseAmenities.Where(ha => !ha.IsDeleted))
                    .ThenInclude(ha => ha.Amenity)
                .Include(h => h.Reviews.Where(r => !r.IsDeleted))
                .Include(h => h.Bookings)
                .Include(h => h.ApplicationUser);
        }





    }
}
