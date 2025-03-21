using Airbnb.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Services.Contract.HouseServices.Contract
{
    public interface IHouseService
    {
        Task<IEnumerable<House>> GetAllHousesAsync();
        Task<House> GetHouseByIdAsync(int id);
        Task AddHouseAsync(House house);
        Task UpdateHouseAsync(House house);
        Task DeleteHouseAsync(int houseId);
        Task<IEnumerable<House>> GetHousesByCityAsync(string city);
        Task<IEnumerable<House>> GetHousesByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<House>> GetAvailableHousesAsync();
        Task<IEnumerable<House>> SearchHousesAsync(string keyword);
    }
}
