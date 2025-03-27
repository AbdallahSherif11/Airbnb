using Airbnb.Core.DTOs.HouseAmenityDTO;
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
        Task AddHouseAsync(CreateHouseDTO createHouseDTO,List<CreateHouseAmenityDTO> createHouseAmenityDTO, List<IFormFile> images);
        Task UpdateHouseAsync(House house);
        Task DeleteHouseAsync(int houseId);
        Task<IEnumerable<ReadHouseDTO>> GetHousesByCityAsync(string city);
        Task<IEnumerable<ReadHouseDTO>> GetHousesByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<ReadHouseDTO>> GetAvailableHousesAsync();
        Task<IEnumerable<ReadHouseDTO>> SearchHousesAsync(string keyword);
    }
}
