using Airbnb.Core.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Airbnb.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Address { get; set; }
        public string ProfilePictureUrl { get; set; } = "/images/users/default/default-profile.png";
        public string NationalId { get; set; }
        public bool IsAgreed { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<House> Houses { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Messages> SentMessages { get; set; }
        public virtual ICollection<Messages> ReceivedMessages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}
