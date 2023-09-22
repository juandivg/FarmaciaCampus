using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ProductoVentaConfiguration : IEntityTypeConfiguration<ProductoVenta>
    {
        public void Configure(EntityTypeBuilder<ProductoVenta> builder)
        {
            builder.HasOne(p=>p.Producto).WithMany(p=>p.ProductoVentas).HasForeignKey(p=>p.IdProductofk);
            builder.HasOne(p=>p.Venta).WithMany(p=>p.ProductoVentas).HasForeignKey(p=>p.IdVentafk);
            builder.Property(p=>p.Cantidad).HasColumnName("Cantidad").HasColumnType("int").IsRequired();
        }
    }
}