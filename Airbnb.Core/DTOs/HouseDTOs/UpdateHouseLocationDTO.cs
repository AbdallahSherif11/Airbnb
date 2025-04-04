using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.HouseDTOs
{
    public class UpdateHouseLocationDTO
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
