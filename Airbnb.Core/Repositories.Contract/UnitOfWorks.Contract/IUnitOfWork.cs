using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract
{
    public interface IUnitOfWork
    {
        public IHouseRepository HouseRepository { get; }
        public IAmenityRepository AmenityRepository { get; }
        public IHouseAmenityRepository HouseAmenityRepository { get; }
        public IReviewRepository ReviewRepository{ get; }
        public IBookingRepository BookingRepository{ get; }

        Task<int> CompleteSaveAsync();

    }
}
