using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace bo.gp.DB.Models
{
    public class FingerprintEventConfiguration : IEntityTypeConfiguration<FingerprintEvent>
    {
        public void Configure(EntityTypeBuilder<FingerprintEvent> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.UserId).IsRequired(true);
            builder.Property(c => c.ComputerName).IsRequired(true).HasMaxLength(30);
            builder.Property(c => c.PhysicalAddress).IsRequired(true).HasMaxLength(20);
            builder.Property(c => c.IP).IsRequired(true).HasMaxLength(20);
            builder.Property(c => c.CreatedOn).IsRequired(true);
            builder.Property(c => c.ConsolidatedOn);
            builder.Property(c => c.Consolidated).IsRequired(true);
        }
    }
}
