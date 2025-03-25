using Airbnb.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Entities.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string GuestId { get; set; }
        public int BookingId { get; set; }
        public int HouseId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual House House { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
