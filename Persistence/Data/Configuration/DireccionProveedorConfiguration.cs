using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class DireccionProveedorConfiguration : IEntityTypeConfiguration<DireccionProveedor>
    {
        public void Configure(EntityTypeBuilder<DireccionProveedor> builder)
        {
            builder.ToTable("DireccionProveedor");
            builder.Property(p=>p.Calle).HasColumnName("CalleProveedor").HasMaxLength(250).IsRequired();
            builder.Property(p=>p.Carrera).HasColumnName("CarreraProveedor").HasMaxLength(250).IsRequired();
            builder.Property(p=>p.Detalles).HasColumnName("DetallesProveedor").HasMaxLength(250).IsRequired();
            builder.HasOne(p=>p.Barrio).WithMany(p=>p.DireccionProveedores).HasForeignKey(p=>p.IdBarriofk);
            

        }
    }
}