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
    public class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.PaymentId);

            builder.Property(p => p.CreatedAt)
                   .IsRequired();

            builder.Property(p => p.PaymentMethod)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.PaymentCode)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.TotalPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Status)
                   .IsRequired();

            builder.Property(p => p.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.HasOne(p => p.Booking)
                   .WithOne(b => b.Payment) 
                   .HasForeignKey<Payment>(p => p.BookingId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
