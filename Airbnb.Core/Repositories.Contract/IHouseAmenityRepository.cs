using Airbnb.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Repositories.Contract
{
    public interface IHouseAmenityRepository
    {
        Task<IEnumerable<HouseAmenity>> GetAllForHouseAsync(int houseId); // For updating House Amenities
        Task<IEnumerable<HouseAmenity>> GetAllAsync();
        Task<HouseAmenity> GetAsync(int houseId, int amenityId);
        Task AddAsync(HouseAmenity houseAmenity);
        void Update(HouseAmenity houseAmenity);
        Task DeleteAsync(int houseId, int amenityId);
    }
}
