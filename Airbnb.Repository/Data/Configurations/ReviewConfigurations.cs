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
    public class ReviewConfigurations : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.ReviewId);

            builder.Property(r => r.GuestId)
                   .IsRequired();

            builder.Property(r => r.HouseId)
                   .IsRequired();

            builder.Property(r => r.BookingId)
                   .IsRequired();

            builder.Property(r => r.Rating)
                   .IsRequired();

            builder.Property(r => r.Comment)
                   .HasMaxLength(1000);

            builder.Property(r => r.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(r => r.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GetUTCDATE()");

            builder.HasOne(r => r.ApplicationUser)
                   .WithMany(u => u.Reviews)
                   .HasForeignKey(r => r.GuestId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.House)
                   .WithMany(h => h.Reviews)
                   .HasForeignKey(r => r.HouseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Booking)
                   .WithOne(b => b.Review) 
                   .HasForeignKey<Review>(r => r.BookingId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
