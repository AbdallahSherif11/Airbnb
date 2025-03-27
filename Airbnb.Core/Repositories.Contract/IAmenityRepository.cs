using Airbnb.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Repositories.Contract
{
    public interface IAmenityRepository
    {
        Task<IEnumerable<Amenity>> GetAllAsync();
        Task<Amenity> GetAsync(int id);
        Task AddAsync(Amenity amenity);
        void Update(Amenity amenity);
        Task DeleteAsync(int id);
    }
}
