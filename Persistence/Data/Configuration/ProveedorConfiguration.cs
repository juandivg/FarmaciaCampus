using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
{
    public void Configure(EntityTypeBuilder<Proveedor> builder)
    {
        builder.ToTable("Proveedor");

        builder.Property(p => p.NombreProveedor).HasColumnName("NombreProveedor").HasMaxLength(250).IsRequired();
        builder.Property(p => p.NIT).HasColumnName("NIT").HasMaxLength(255).IsRequired();
        builder.Property( p => p.Correo).HasColumnName("Email").HasMaxLength(255).IsRequired();
        builder.HasOne(p => p.DireccionPro).WithMany(p => p.Proveedores).HasForeignKey(p => p.IdDireccionProFK);
    }
}
