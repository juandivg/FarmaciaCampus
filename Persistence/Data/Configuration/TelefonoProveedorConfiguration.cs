using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class TelefonoProveedorConfiguration : IEntityTypeConfiguration<TelefonoProveedor>
{
    public void Configure(EntityTypeBuilder<TelefonoProveedor> builder)
    {
        builder.ToTable("telefonoProveedor");

        builder.Property(p => p.Numero).HasColumnName("Numero").IsRequired();
        builder.HasOne(p => p.TipoTelefono).WithMany(p => p.TelefonoProveedores).HasForeignKey(p => p.IdTipoTelefonofk);
        builder.HasOne(p => p.Proveedor).WithMany(p => p.TelefonoProveedores).HasForeignKey(p => p.IdProveedorfk);
    }
}
