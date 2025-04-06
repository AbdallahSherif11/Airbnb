using Airbnb.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Repositories.Contract
{
    public interface IBookingRepository
    {
        Task<bool> AnyAsync(Expression<Func<Booking, bool>> predicate);
        Task AddAsync(Booking booking);
        Task<Booking> GetByIdAsync(int id);
    }
}
