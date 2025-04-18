using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.ReviewDTOs
{
    public class DetailedReadReviewDTO
    {

        public int ReviewId { get; set; }

        public int BookingId { get; set; }

        public string ReviewerName { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
    }
}
