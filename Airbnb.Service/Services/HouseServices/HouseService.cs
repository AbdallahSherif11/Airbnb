using Airbnb.Core.Entities.Models;
using Airbnb.Core.Services.Contract.HouseServices.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Service.Services.HouseServices
{
    public class HouseService : IHouseService
    {
        public Task AddHouseAsync(House house)
        {
            throw new NotImplementedException();
        }

        public Task DeleteHouseAsync(int houseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<House>> GetAllHousesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<House>> GetAvailableHousesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<House> GetHouseByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<House>> GetHousesByCityAsync(string city)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<House>> GetHousesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<House>> SearchHousesAsync(string keyword)
        {
            throw new NotImplementedException();
        }

        public Task UpdateHouseAsync(House house)
        {
            throw new NotImplementedException();
        }
    }
}
