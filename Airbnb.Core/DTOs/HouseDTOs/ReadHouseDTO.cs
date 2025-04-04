using Airbnb.Core.DTOs.BookingDTOs;
using Airbnb.Core.DTOs.ReviewDTOs;
using Airbnb.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.HouseDTOs
{
    public class ReadHouseDTO
    {
        public int HouseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAvailable { get; set; }
        public int MaxDays { get; set; }
        public int MaxGuests { get; set; }
        public string HouseView { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBeds { get; set; }
        public List<string> Images { get; set; }
        public List<string> Amenities { get; set; }
        public List<ReadReviewDTO> Reviews { get; set; }
        public List<ReadBookingDTO> Bookings { get; set; }

        public string HostName { get; set; }

    }
}
