using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airbnb.Core.Entities.Models;

namespace Airbnb.Repository.Data.Configurations
{
    public class ImageConfigurations : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.ImageId);

            builder.Property(i => i.Url)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.HasOne(i => i.House)
                   .WithMany(h => h.Images)
                   .HasForeignKey(i => i.HouseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
