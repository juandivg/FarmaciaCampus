using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class ProveedorProductoConfiguration : IEntityTypeConfiguration<ProveedorProducto>
{
    public void Configure(EntityTypeBuilder<ProveedorProducto> builder)
    {
        builder.ToTable("ProveedorProducto");

        builder.Property(p => p.FechaVencimiento).HasColumnName("FechaVencimiento").IsRequired();
        builder.HasOne(p => p.Proveedor).WithMany(p => p.ProveedorProductos).HasForeignKey(p => p.IdProveedorfk);
        builder.HasOne(p => p.Producto).WithMany(p => p.ProveedorProductos).HasForeignKey(p => p.IdProductofk);

    }
}
