﻿using Airbnb.Core.DTOs.HouseAmenityDTO;
using Airbnb.Core.DTOs.HouseDTOs;
using Airbnb.Core.Entities.Models;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Core.Services.Contract.HouseServices.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private readonly IImageService _imageService;


        public HouseService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
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

        public async Task<ReadHouseDTO> GetHouseByIdForUpdate(int id)
        {
            var house = await _unitOfWork.HouseRepository.GetAsyncForUpdate(id);
            return _mapper.Map<ReadHouseDTO>(house);
        }

        public async Task<IEnumerable<ReadHouseDTO>> GetHousesByHostAsync(string hostId)
        {
            var houses = await _unitOfWork.HouseRepository.GetHousesByConditionAsync(h => h.HostId == hostId && !h.IsDeleted);

            if (!houses.Any())
                throw new Exception("No houses found for the current user.");

            return _mapper.Map<IEnumerable<ReadHouseDTO>>(houses);
        }

        public async Task AddHouseAsync(CreateHouseDTO createHouseDTO)
        {
            var house = _mapper.Map<House>(createHouseDTO);
            await _unitOfWork.HouseRepository.AddAsync(house);
            await _unitOfWork.CompleteSaveAsync();

            foreach (var image in createHouseDTO.ImagesList)
            {
                var imagePath = await _imageService.SaveHouseImageAsync(image, house.HouseId);
                var imageEntity = new Image
                {
                    Url = imagePath,
                    HouseId = house.HouseId,
                };
                await _unitOfWork.HouseRepository.AddImageAsync(house.HouseId, imageEntity);
            }

            foreach (var x in createHouseDTO.AmenitiesList)
            {
                var houseAmenityEntity = new HouseAmenity
                {
                    AmenityId = x,
                    HouseId = house.HouseId,
                };
                await _unitOfWork.HouseAmenityRepository.AddAsync(houseAmenityEntity);
            }

            await _unitOfWork.CompleteSaveAsync();
        }



        public async Task DeleteHouseAsync(int houseId)
        {
            await _unitOfWork.HouseRepository.DeleteAsync(houseId);
            await _unitOfWork.CompleteSaveAsync(); 
        }

        public async Task<IEnumerable<ReadHouseDTO>> GetAvailableHousesAsync()
        {
            var houses = await _unitOfWork.HouseRepository.GetAvailableHousesAsync();
            return _mapper.Map<IEnumerable<ReadHouseDTO>>(houses);
        }

        public async Task<IEnumerable<ReadHouseDTO>> GetHousesByCityAsync(string city)
        {
            var houses = await _unitOfWork.HouseRepository.GetHousesByCityAsync(city);
            return _mapper.Map<IEnumerable<ReadHouseDTO>>(houses);
        }

        public async Task<IEnumerable<ReadHouseDTO>> GetHousesByViewAsync(string view)
        {
            var houses = await _unitOfWork.HouseRepository.GetHousesByViewAsync(view);
            return _mapper.Map<IEnumerable<ReadHouseDTO>>(houses);
        }

        public async Task<IEnumerable<ReadHouseDTO>> GetHousesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var houses = await _unitOfWork.HouseRepository.GetHousesByPriceRangeAsync(minPrice, maxPrice);
            return _mapper.Map<IEnumerable<ReadHouseDTO>>(houses);
        }

        public async Task<IEnumerable<ReadHouseDTO>> SearchHousesAsync(string keyword)
        {
            var houses = await _unitOfWork.HouseRepository.SearchHousesAsync(keyword);
            return _mapper.Map<IEnumerable<ReadHouseDTO>>(houses);
        }


        public async Task AddHouseImagesAsync(int houseId, List<IFormFile> images)
        {
            var house = await _unitOfWork.HouseRepository.GetAsync(houseId);
            if (house == null) throw new KeyNotFoundException("House not found");

            foreach (var image in images)
            {
                var imagePath = await _imageService.SaveHouseImageAsync(image, houseId);
                var imageEntity = new Image
                {
                    Url = imagePath,
                    HouseId = houseId,
                };
                await _unitOfWork.HouseRepository.AddImageAsync(houseId, imageEntity);
            }

            await _unitOfWork.CompleteSaveAsync();
        }

        public async Task RemoveHouseImageAsync(int imageId)
        {
            var image = await _unitOfWork.HouseRepository.GetImageAsync(imageId);
            if (image != null)
            {
                await _imageService.DeleteHouseImageAsync(image.Url);
                await _unitOfWork.HouseRepository.DeleteImageAsync(imageId);
                await _unitOfWork.CompleteSaveAsync();
            }
        }


        #region Update House Region

        public async Task UpdateHouseTitle(int houseId, string title)
        {
            var house = await _unitOfWork.HouseRepository.GetAsyncForUpdate(houseId);

            house.Title = title;
            _unitOfWork.HouseRepository.Update(house);
            await _unitOfWork.CompleteSaveAsync();
        }

        public async Task UpdateHouseDescription(int houseId, string description)
        {
            var house = await _unitOfWork.HouseRepository.GetAsyncForUpdate(houseId);

            house.Description = description;
            _unitOfWork.HouseRepository.Update(house);
            await _unitOfWork.CompleteSaveAsync();
        }

        public async Task UpdateHousePricePerNight(int houseId, decimal PricePerNight)
        {
            var house = await _unitOfWork.HouseRepository.GetAsyncForUpdate(houseId);

            house.PricePerNight = PricePerNight;
            _unitOfWork.HouseRepository.Update(house);
            await _unitOfWork.CompleteSaveAsync();
        }

        public async Task UpdateHouseLocation(int houseId, UpdateHouseLocationDTO updateHouseLocationDTO)
        {
            var house = await _unitOfWork.HouseRepository.GetAsyncForUpdate(houseId);

            house.Country = updateHouseLocationDTO.Country;
            house.City = updateHouseLocationDTO.City;
            house.Street = updateHouseLocationDTO.Street;
            house.Latitude = updateHouseLocationDTO.Latitude;
            house.Longitude = updateHouseLocationDTO.Longitude;

            _unitOfWork.HouseRepository.Update(house);
            await _unitOfWork.CompleteSaveAsync();
        }

        public async Task UpdateHouseAvailability(int houseId, UpdateHouseAvailabilityDTO updateHouseAvailabilityDTO)
        {
            var house = await _unitOfWork.HouseRepository.GetAsyncForUpdate(houseId);

            house.IsAvailable = updateHouseAvailabilityDTO.IsAvailable;
            house.MaxDays = updateHouseAvailabilityDTO.MaxDays;
            house.MaxGuests = updateHouseAvailabilityDTO.MaxGuests;
            house.HouseView = updateHouseAvailabilityDTO.HouseView;
            house.NumberOfRooms = updateHouseAvailabilityDTO.NumberOfRooms;
            house.NumberOfBeds = updateHouseAvailabilityDTO.NumberOfBeds;

            _unitOfWork.HouseRepository.Update(house);
            await _unitOfWork.CompleteSaveAsync();
        }

        public async Task UpdateHouseImages(int houseId, List<IFormFile> images)
        {
            var house = await _unitOfWork.HouseRepository.GetAsyncForUpdate(houseId);
            
            foreach(var OldImage in house.Images)
            {
                OldImage.IsDeleted = true;
            }

            foreach (var image in images)
            {
                var imagePath = await _imageService.SaveHouseImageAsync(image, house.HouseId);
                var imageEntity = new Image
                {
                    Url = imagePath,
                    HouseId = house.HouseId,
                };
                await _unitOfWork.HouseRepository.AddImageAsync(house.HouseId, imageEntity);
            }

            await _unitOfWork.CompleteSaveAsync();
        }

        //public async Task UpdateHouseAmenitiesAsync(UpdateHouseAmenityDTO updateHouseAmenityDTO)
        //{
        //    var house = await _unitOfWork.HouseRepository.GetAsync(updateHouseAmenityDTO.HouseId);
        //    if (house == null)
        //    {
        //        throw new KeyNotFoundException("House not found");
        //    }

        //    // Remove existing amenities
        //    var existingAmenities = await _unitOfWork.HouseAmenityRepository.GetAllAsync();
        //    var houseAmenities = existingAmenities.Where(ha => ha.HouseId == updateHouseAmenityDTO.HouseId).ToList();
        //    foreach (var houseAmenity in houseAmenities)
        //    {
        //        await _unitOfWork.HouseAmenityRepository.DeleteAsync(houseAmenity.HouseId, houseAmenity.AmenityId);
        //    }

        //    // Add new amenities
        //    foreach (var amenityId in updateHouseAmenityDTO.AmenityIds)
        //    {
        //        var houseAmenity = new HouseAmenity
        //        {
        //            HouseId = updateHouseAmenityDTO.HouseId,
        //            AmenityId = amenityId
        //        };
        //        await _unitOfWork.HouseAmenityRepository.AddAsync(houseAmenity);
        //    }

        //    await _unitOfWork.CompleteSaveAsync();
        //}

        public async Task UpdateHouseAmenitiesAsync(UpdateHouseAmenityDTO updateHouseAmenityDTO)
        {
            var house = await _unitOfWork.HouseRepository.GetAsyncForUpdate(updateHouseAmenityDTO.HouseId);
            if (house == null)
            {
                throw new KeyNotFoundException("House not found");
            }

            // Get current amenities (including soft-deleted ones)
            var existingAmenities = await _unitOfWork.HouseAmenityRepository.GetAllForHouseAsync(updateHouseAmenityDTO.HouseId);

            // Process amenities to add (those that don't exist or were previously deleted)
            foreach (var amenityId in updateHouseAmenityDTO.AmenityIds)
            {
                var existingAmenity = existingAmenities.FirstOrDefault(ha => ha.AmenityId == amenityId);

                if (existingAmenity == null)
                {
                    // Add new amenity
                    var houseAmenity = new HouseAmenity
                    {
                        HouseId = updateHouseAmenityDTO.HouseId,
                        AmenityId = amenityId,
                        IsDeleted = false
                    };
                    await _unitOfWork.HouseAmenityRepository.AddAsync(houseAmenity);
                }
                else if (existingAmenity.IsDeleted)
                {
                    // Restore previously deleted amenity
                    existingAmenity.IsDeleted = false;
                    _unitOfWork.HouseAmenityRepository.Update(existingAmenity);
                }
                // If it exists and not deleted, no action needed
            }

            // Process amenities to remove (those not in the new list but exist in DB)
            foreach (var existingAmenity in existingAmenities)
            {
                if (!updateHouseAmenityDTO.AmenityIds.Contains(existingAmenity.AmenityId)
                    && !existingAmenity.IsDeleted)
                {
                    await _unitOfWork.HouseAmenityRepository.DeleteAsync(existingAmenity.HouseId, existingAmenity.AmenityId);
                }
            }

            await _unitOfWork.CompleteSaveAsync();
        }


        #endregion

    }
}
