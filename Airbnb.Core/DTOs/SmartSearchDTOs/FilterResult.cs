using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.SmartSearchDTOs
{
    public class FilterResult
    {
        public string? Country { get; set; }
        public string? City { get; set; }
        public int? Rooms { get; set; }
        public int? Beds { get; set; }
        public List<string>? Amenities { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? HouseView { get; set; }
    }
}
