using Airbnb.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Repositories.Contract
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
        Task<Payment> GetByIdAsync(int id);
        Task<Payment> GetByBookingIdAsync(int bookingId);
        Task SaveChangesAsync();
    }
}
