using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.ReviewDTOs
{
    public class UpdateReviewDTO
    {
        public int ReviewId { get; set; }         
        public int Rating { get; set; }           
        public string? Comment { get; set; }      
    }
}
