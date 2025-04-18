using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.BookingDTOs
{
    public class DetailedReadBookingDTO
    {
        public int BookingId { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public string HouseTitle { get; set; }
        public string HouseAddress { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
