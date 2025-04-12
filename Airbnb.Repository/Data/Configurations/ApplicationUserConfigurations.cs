using Airbnb.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Repository.Data.Configurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(P => P.FirstName)
                   .IsRequired()
                   .HasMaxLength(40);

            builder.Property(P => P.LastName)
                   .IsRequired()
                   .HasMaxLength(40);

            builder.Property(P => P.DateOfBirth)
                   .IsRequired();

            builder.Property(u => u.PhoneNumber)
               .IsRequired()
               .HasMaxLength(14)
               .HasAnnotation("MinLength", 11);

            builder.Property(P => P.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GetUTCDATE()");

            builder.Property(P => P.Address)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(P => P.NationalId)
                   .IsRequired()
                   .HasMaxLength(14);

            builder.HasIndex(P => P.NationalId)
                   .IsUnique();

            builder.Property(P => P.IsAgreed)
                   .IsRequired();

            builder.Property(u => u.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }
}
