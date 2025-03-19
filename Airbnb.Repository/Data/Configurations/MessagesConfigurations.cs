using Airbnb.Core.Entities.Identity;
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
    class MessagesConfigurations : IEntityTypeConfiguration<Messages>
    {
        public void Configure(EntityTypeBuilder<Messages> builder)
        {
            builder.HasKey(m => m.MessageId);

            builder.Property(m => m.MessageContent)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(m => m.TimeStamp)
                   .IsRequired()
                   .HasDefaultValueSql("GetUTCDATE()");

            builder.Property(m => m.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.HasOne(m => m.Sender)
                   .WithMany(u => u.SentMessages)
                   .HasForeignKey(m => m.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Receiver)
                   .WithMany(u => u.ReceivedMessages)
                   .HasForeignKey(m => m.ReceiverId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
