using Airbnb.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Repositories.Contract
{
    public interface IHouseRepository 
    {
        Task<IEnumerable<House>> GetAllAsync();
        Task<House> GetAsync(int id);
        Task AddAsync(House house);
        void Update(House house);
        Task DeleteAsync(int id);


        Task<IEnumerable<House>>  GetHousesByCityAsync(string city);
        Task<IEnumerable<House>> GetHousesByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<House>> GetAvailableHousesAsync();
        Task<IEnumerable<House>> SearchHousesAsync(string keyword);
    }
}
