using Airbnb.Core.DTOs.HouseDTOs;
using Airbnb.Core.DTOs.SmartSearchDTOs;
using Airbnb.Core.Helpers;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Core.Services.Contract.SmartSearchService.Contract;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Service.Services.SearchService
{
    namespace Airbnb.Service.Services.SearchService
    {
        public class SmartSearchService : ISmartSearchService
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public SmartSearchService(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ReadHouseDTO>> SmartSearchAsync(FilterResult filters, string? keyword = null)
            {
                var query = _unitOfWork.HouseRepository.GetQueryableWithIncludes();

                if (!string.IsNullOrWhiteSpace(filters.City))
                    query = query.Where(h => h.City.ToLower().Contains(filters.City.ToLower()));

                if (!string.IsNullOrWhiteSpace(filters.Country))
                    query = query.Where(h => h.Country.ToLower().Contains(filters.Country.ToLower()));

                if (filters.Rooms.HasValue)
                    query = query.Where(h => h.NumberOfRooms >= filters.Rooms.Value);

                if (filters.Beds.HasValue)
                    query = query.Where(h => h.NumberOfBeds >= filters.Beds.Value);

                if (filters.MinPrice.HasValue)
                    query = query.Where(h => h.PricePerNight >= filters.MinPrice.Value);

                if (filters.MaxPrice.HasValue)
                    query = query.Where(h => h.PricePerNight <= filters.MaxPrice.Value);

                if (!string.IsNullOrWhiteSpace(filters.HouseView))
                    query = query.Where(h => h.HouseView.ToLower().Contains(filters.HouseView.ToLower()));

                if (filters.Amenities is { Count: > 0 })
                {
                    var amenityIds = filters.Amenities
                        .Where(a => AmenityMapper.NameToId.ContainsKey(a))
                        .Select(a => AmenityMapper.NameToId[a])
                        .ToList();

                    var houseIdsWithAllAmenities = await _unitOfWork.HouseRepository
                        .GetQueryable()
                        .Where(h => h.HouseAmenities.Any(ha => amenityIds.Contains(ha.AmenityId) && !ha.IsDeleted))
                        .GroupBy(h => h.HouseId)
                        .Where(g => amenityIds.All(id => g.SelectMany(h => h.HouseAmenities).Any(ha => ha.AmenityId == id)))
                        .Select(g => g.Key)
                        .ToListAsync();

                    query = query.Where(h => houseIdsWithAllAmenities.Contains(h.HouseId));
                }

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    var lowered = keyword.ToLower();
                    query = query.Where(h =>
                        h.Title.ToLower().Contains(lowered) ||
                        h.Description.ToLower().Contains(lowered) ||
                        h.City.ToLower().Contains(lowered) ||
                        h.Street.ToLower().Contains(lowered));
                }

                var houses = await query.ToListAsync();
                return _mapper.Map<IEnumerable<ReadHouseDTO>>(houses);
            }
        }
    }






}
