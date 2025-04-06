using Airbnb.Core.DTOs.BookingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Services.Contract.BookingServices.Contract
{
    public interface IBookingService
    {
        Task<ReadBookingDTO> CreateBookingAsync(CreateBookingDTO dto, string guestId);
    }
}
