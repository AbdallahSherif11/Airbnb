using Airbnb.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Entities.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string GuestId { get; set; }
        public int HouseId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsDeleted { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public House House { get; set; }
        public Review Review { get; set; }
        public Payment Payment { get; set; }
    }
}
