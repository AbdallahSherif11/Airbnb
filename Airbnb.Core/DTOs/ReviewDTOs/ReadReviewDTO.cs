using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.ReviewDTOs
{
    public class ReadReviewDTO
    {
        public string ReviewerName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
