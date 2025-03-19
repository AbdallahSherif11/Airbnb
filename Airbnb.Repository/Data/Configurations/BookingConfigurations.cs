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
    public class BookingConfigurations : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.BookingId);

            builder.Property(b => b.GuestId)
                   .IsRequired();

            builder.Property(b => b.HouseId)
                   .IsRequired();

            builder.Property(b => b.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GetUTCDATE()");

            builder.Property(b => b.CheckInDate)
                   .IsRequired();

            builder.Property(b => b.CheckOutDate)
                   .IsRequired();

            builder.Property(b => b.TotalPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(b => b.PaymentMethod)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(b => b.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.HasOne(b => b.ApplicationUser)
                   .WithMany(u => u.Bookings)
                   .HasForeignKey(b => b.GuestId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.House)
                   .WithMany(h => h.Bookings)
                   .HasForeignKey(b => b.HouseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
