using Airbnb.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.SmartSearchDTOs
{
    public class SmartSearchQueryDTO
    {
        public string? Country { get; set; }
        public string? City { get; set; }
        public int? Rooms { get; set; }
        public int? Beds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? HouseView { get; set; }
        public List<string>? Amenities { get; set; }

        public string? Keyword { get; set; } // still useful for keyword search

        public FilterResult ToFilterResult()
        {
            return new FilterResult
            {
                Country = this.Country,
                City = this.City,
                Rooms = this.Rooms,
                Beds = this.Beds,
                MinPrice = this.MinPrice,
                MaxPrice = this.MaxPrice,
                HouseView = this.HouseView,
                Amenities = this.Amenities ?? new List<string>()
            };
        }
    }


}