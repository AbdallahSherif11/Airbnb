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
        Task AddHouseAsync(CreateHouseDTO createHouseDTO);
        Task DeleteHouseAsync(int houseId);
        Task<IEnumerable<ReadHouseDTO>> GetHousesByCityAsync(string city);
        Task<IEnumerable<ReadHouseDTO>> GetHousesByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<ReadHouseDTO>> GetAvailableHousesAsync();
        Task<IEnumerable<ReadHouseDTO>> SearchHousesAsync(string keyword);


        #region Update House Region
        //Task UpdateHouseAsync(House house);

        Task UpdateHouseTitle(int houseId, string title);
        Task UpdateHouseDescription(int houseId, string description);
        Task UpdateHousePricePerNight(int houseId, decimal PricePerNight);
        Task UpdateHouseLocation(int houseId, UpdateHouseLocationDTO updateHouseLocationDTO);
        Task UpdateHouseAvailability(int houseId, UpdateHouseAvailabilityDTO updateHouseAvailabilityDTO);
        Task UpdateHouseImages(int houseId, List<IFormFile> images);
        Task UpdateHouseAmenitiesAsync(UpdateHouseAmenityDTO updateHouseAmenityDTO);
        #endregion
    }
}
