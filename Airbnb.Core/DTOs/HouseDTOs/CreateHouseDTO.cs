using Airbnb.Core.DTOs.HouseAmenityDTO;
using Airbnb.Core.Entities.Identity;
using Airbnb.Core.Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.HouseDTOs
{
    public class CreateHouseDTO
    {
        public string HostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsAvailable { get; set; }
        public int MaxDays { get; set; }
        public int MaxGuests { get; set; }
        public string HouseView { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBeds { get; set; }
        public List<IFormFile> ImagesList { get; set; }
        public List<int> AmenitiesList { get; set; }
    }
}
