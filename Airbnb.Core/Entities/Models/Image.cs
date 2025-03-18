using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Entities.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Url { get; set; }
        public int HouseId { get; set; }
        public House House { get; set; }
    }
}
