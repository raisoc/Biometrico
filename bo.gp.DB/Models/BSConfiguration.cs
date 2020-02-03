using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace bo.gp.DB.Models
{

    public class BSConfiguration : IEntityTypeConfiguration<BS>
    {
        public void Configure(EntityTypeBuilder<BS> builder)
        {
            builder.HasKey(p => new { p.UserID, p.eventTime, p.eventCode });
            
            builder.HasIndex(p => new { p.UserID, p.eventTime,p.eventCode });
            builder.Property(c => c.UserID).IsRequired(true);
            builder.Property(c => c.eventTime).IsRequired(true);
            builder.Property(c => c.eventCode).IsRequired(true);
            builder.Property(c => c.tnaEvent).IsRequired(true);
            builder.Property(c => c.Code).HasMaxLength(20);
            builder.Property(c => c.IP).HasMaxLength(15);
        }
    }
}
