﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Entities.Models
{
    public class HouseAmenity
    {
        public int HouseId { get; set; }
        public virtual House House { get; set; }
        public int AmenityId { get; set; }
        public virtual Amenity Amenity { get; set; }
        public bool IsDeleted { get; set; }

    }
}
