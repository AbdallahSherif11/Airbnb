using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.HouseAmenityDTO
{
    public class UpdateHouseAmenityDTO
    {
        public int HouseId { get; set; }
        public List<int> AmenityIds { get; set; }
    }
}
