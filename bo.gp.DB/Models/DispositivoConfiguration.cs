using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace bo.gp.DB.Models
{

    public class DispositivoConfiguration : IEntityTypeConfiguration<Dispositivo>
    {
        public void Configure(EntityTypeBuilder<Dispositivo> builder)
        {
            builder.HasKey(c => c.DireccionIP);
            builder.Property(c => c.Puerto).IsRequired(true);
            builder.Property(c => c.Cod_Estado).IsRequired(true).HasMaxLength(1);
            builder.Property(c => c.Tipo).IsRequired(true).HasMaxLength(1);
            builder.Property(c => c.TipoId).IsRequired(true).HasMaxLength(1);
            builder.Property(c => c.Comentario).IsRequired(true).HasMaxLength(100);
            builder.Property(c => c.Param).HasMaxLength(200);
        }
    }
}
