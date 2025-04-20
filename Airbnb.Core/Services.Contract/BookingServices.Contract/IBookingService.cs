using Airbnb.Core.DTOs.BookingDTOs;
using Airbnb.Core.DTOs.PaymentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Services.Contract.BookingServices.Contract
{
    public interface IBookingService
    {
        Task<ReadBookingForPaymentDTO> CreateBookingAsync(CreateBookingDTO dto, string guestId);

        Task<IEnumerable<DetailedReadBookingDTO>> GetDetailedBookingsAsHostAsync(string hostId);
        Task<IEnumerable<DetailedReadBookingDTO>> GetDetailedBookingsAsGuestAsync(string guestId);
    }
}
