using Airbnb.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Repository.Data.Configurations
{
    public class HouseAmenityConfigurations : IEntityTypeConfiguration<HouseAmenity>
    {
        public void Configure(EntityTypeBuilder<HouseAmenity> builder)
        {
            builder.HasKey(ha => new { ha.HouseId, ha.AmenityId });

            builder.Property(h => h.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.HasOne(ha => ha.House)
                   .WithMany(h => h.HouseAmenities)
                   .HasForeignKey(ha => ha.HouseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ha => ha.Amenity)
                   .WithMany(a => a.HouseAmenities)
                   .HasForeignKey(ha => ha.AmenityId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
