using Airbnb.Core.Entities.Models;
using Airbnb.Core.Repositories.Contract;
using Airbnb.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Repository.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AirbnbDbContext _context;


        public PaymentRepository(AirbnbDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.PaymentId == id && !p.IsDeleted);
        }

        public async Task<Payment> GetByBookingIdAsync(int bookingId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.BookingId == bookingId && !p.IsDeleted);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
