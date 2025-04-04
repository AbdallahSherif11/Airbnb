using Airbnb.Core.Entities.Identity;
using Airbnb.Core.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Repository.Data.Contexts
{
    public class AirbnbDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<WishList> WishLists { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Amenity> Amenities { get; set; }
        public virtual DbSet<HouseAmenity> HouseAmenities { get; set; }
        public virtual DbSet<Payment> Payments{ get; set; }


        public AirbnbDbContext(DbContextOptions<AirbnbDbContext> options) : base(options) 
        {

        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
