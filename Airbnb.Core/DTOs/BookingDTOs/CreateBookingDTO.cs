using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.BookingDTOs
{
    public class CreateBookingDTO
    {
        public int HouseId { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public string PaymentMethod { get; set; }
    }
}
