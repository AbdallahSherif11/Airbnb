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
    public class HouseConfigurations : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder.HasKey(h => h.HouseId);

            builder.Property(h => h.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(h => h.Description)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(h => h.PricePerNight)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(h => h.Country)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(h => h.City)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(h => h.Street)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(h => h.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GetUTCDATE()");

            builder.Property(h => h.IsAvailable)
                   .IsRequired();

            builder.Property(h => h.MaxDays)
                   .IsRequired();

            builder.Property(h => h.MaxGuests)
                   .IsRequired();

            builder.Property(h => h.View)
                   .HasMaxLength(50);

            builder.Property(h => h.NumberOfRooms)
                   .IsRequired();

            builder.Property(h => h.NumberOfBeds)
                   .IsRequired();

            builder.Property(h => h.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.HasMany(h => h.Images)
                   .WithOne(i => i.House)
                   .HasForeignKey(i => i.HouseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.ApplicationUser)
                   .WithMany(u => u.Houses)
                   .HasForeignKey(h => h.HostId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
