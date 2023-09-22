using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.ToTable("producto");

        builder.Property(p => p.NombreProducto).HasColumnName("NombreProducto").HasMaxLength(255).IsRequired();
        builder.Property(p => p.Stock).HasColumnName("Stock").HasColumnType("int").IsRequired();
        builder.Property(p => p.PrecioC).HasColumnName("PrecioCompra").HasColumnType("decimal").IsRequired();
        builder.Property(p => p.PrecioV).HasColumnName("PrecioVenta").HasColumnType("decimal").IsRequired();
        builder.HasOne(p => p.TipoProducto).WithMany(p => p.Productos).HasForeignKey(p => p.IdTipoProductofk);

        builder
        .HasMany(p => p.Recetas)
        .WithMany(p => p.Productos)
        .UsingEntity<ProductoReceta>(
            j => j
            .HasOne(p => p.Receta)
            .WithMany(p => p.ProductoRecetas)
            .HasForeignKey(p => p.IdRecetafk),

            j => j
            .HasOne(p => p.Producto)
            .WithMany(p => p.ProductoRecetas)
            .HasForeignKey(p => p.IdProductofk),

            j => {
                j.HasKey(p => new {p.IdRecetafk,p.IdProductofk});
            }
        );
    }
}
