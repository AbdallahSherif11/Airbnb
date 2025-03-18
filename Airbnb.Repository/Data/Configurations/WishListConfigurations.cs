using Airbnb.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Repository.Data.Configurations
{
    public class WishListConfigurations : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.HasKey(w => w.WishListId);

            builder.Property(w => w.HouseId)
                   .IsRequired();

            builder.Property(w => w.GuestId)
                   .IsRequired();

            builder.HasOne(w => w.ApplicationUser)
                   .WithMany(u => u.WishLists)
                   .HasForeignKey(w => w.GuestId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
