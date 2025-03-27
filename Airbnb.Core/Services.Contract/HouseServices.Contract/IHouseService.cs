using Airbnb.Core.DTOs.HouseDTOs;
using Airbnb.Core.Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Services.Contract.HouseServices.Contract
{
    public interface IHouseService
    {
        Task<IEnumerable<ReadHouseDTO>> GetAllHousesAsync();
        Task<ReadHouseDTO> GetHouseByIdAsync(int id);
        Task AddHouseAsync(CreateHouseDTO createHouseDTO, List<IFormFile> images);
        Task UpdateHouseAsync(House house);
        Task DeleteHouseAsync(int houseId);
        Task<IEnumerable<House>> GetHousesByCityAsync(string city);
        Task<IEnumerable<House>> GetHousesByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<House>> GetAvailableHousesAsync();
        Task<IEnumerable<House>> SearchHousesAsync(string keyword);
    }
}
