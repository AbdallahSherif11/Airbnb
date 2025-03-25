using Airbnb.Core.DTOs.HouseDTOs;
using Airbnb.Core.Entities.Models;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Core.Services.Contract.HouseServices.Contract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Service.Services.HouseServices
{
    public class HouseService : IHouseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HouseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadHouseDTO>> GetAllHousesAsync()
        {
            var houses = await _unitOfWork.HouseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadHouseDTO>>(houses);
        }

        public async Task<ReadHouseDTO> GetHouseByIdAsync(int id)
        {
            var house = await _unitOfWork.HouseRepository.GetAsync(id);
            return _mapper.Map<ReadHouseDTO>(house);
        }

        public async Task AddHouseAsync(House house)
        {
            await _unitOfWork.HouseRepository.AddAsync(house);
            await _unitOfWork.CompleteSaveAsync(); 
        }

        public async Task UpdateHouseAsync(House house)
        {
            _unitOfWork.HouseRepository.Update(house);
            await _unitOfWork.CompleteSaveAsync(); 
        }

        public async Task DeleteHouseAsync(int houseId)
        {
            await _unitOfWork.HouseRepository.DeleteAsync(houseId);
            await _unitOfWork.CompleteSaveAsync(); 
        }

        public async Task<IEnumerable<House>> GetAvailableHousesAsync()
        {
            return await _unitOfWork.HouseRepository.GetAvailableHousesAsync();
        }

        public async Task<IEnumerable<House>> GetHousesByCityAsync(string city)
        {
            return await _unitOfWork.HouseRepository.GetHousesByCityAsync(city);
        }

        public async Task<IEnumerable<House>> GetHousesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _unitOfWork.HouseRepository.GetHousesByPriceRangeAsync(minPrice, maxPrice);
        }

        public async Task<IEnumerable<House>> SearchHousesAsync(string keyword)
        {
            return await _unitOfWork.HouseRepository.SearchHousesAsync(keyword);
        }

    }
}
