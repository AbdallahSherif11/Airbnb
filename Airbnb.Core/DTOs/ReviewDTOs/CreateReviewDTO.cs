using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.ReviewDTOs
{
    public class CreateReviewDTO
    {
        public string GuestId { get; set; }       
        public int BookingId { get; set; }        
        public int HouseId { get; set; }          
        public int Rating { get; set; }          
        public string? Comment { get; set; }      
    }
}
