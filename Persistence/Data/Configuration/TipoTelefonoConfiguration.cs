using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class TipoTelefonoConfiguration : IEntityTypeConfiguration<TipoTelefono>
{
    public void Configure(EntityTypeBuilder<TipoTelefono> builder)
    {
        builder.ToTable("tipoTelefono");

        builder.Property(p => p.Tipo).HasColumnName("TipoTelefono").HasMaxLength(100).IsRequired();
    }
}
