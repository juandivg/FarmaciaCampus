using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class ProductoCompraConfiguration : IEntityTypeConfiguration<ProductoCompra>
{
    public void Configure(EntityTypeBuilder<ProductoCompra> builder)
    {
        builder.ToTable("ProductoCompra");

        builder.Property(p => p.Cantidad).HasColumnName("Cantidad").HasColumnType("int").IsRequired();
        builder.HasOne(p => p.Producto).WithMany(p => p.ProductoCompras).HasForeignKey(p => p.IdProductofk);
        builder.HasOne(p => p.Compra).WithMany(p => p.productoCompras).HasForeignKey(p => p.IdComprafk);
    }
}
