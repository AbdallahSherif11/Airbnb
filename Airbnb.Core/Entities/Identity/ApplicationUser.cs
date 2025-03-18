using Airbnb.Core.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Address { get; set; }
        public string NationalId { get; set; }
        public bool IsAgreed { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<House> Houses { get; set; }
        public ICollection<WishList> WishLists { get; set; }

    }
}
