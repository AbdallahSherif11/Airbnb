using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.HouseDTOs
{
    public class UpdateHouseAvailabilityDTO
    {
        public bool IsAvailable { get; set; }
        public int MaxDays { get; set; }
        public int MaxGuests { get; set; }
        public string HouseView { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBeds { get; set; }
    }
}
