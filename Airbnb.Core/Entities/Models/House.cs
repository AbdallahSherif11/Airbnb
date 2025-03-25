using Airbnb.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Airbnb.Core.Entities.Models
{
    public class House
    {
        public int HouseId { get; set; }
        public string HostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsAvailable { get; set; }
        public int MaxDays { get; set; }
        public int MaxGuests { get; set; }
        public string View { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBeds { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<HouseAmenity> HouseAmenities { get; set; }
    }
}
