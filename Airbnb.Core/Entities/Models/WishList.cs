using Airbnb.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Entities.Models
{
    public class WishList
    {
        public int WishListId { get; set; }
        public int HouseId { get; set; }
        public string GuestId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
