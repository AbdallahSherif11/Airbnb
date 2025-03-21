using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.BookingDTOs
{
    public class ReadBookingDTO
    {
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public string GuestName { get; set; }
    }
}
