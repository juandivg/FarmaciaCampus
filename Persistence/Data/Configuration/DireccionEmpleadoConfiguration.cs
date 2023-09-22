using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class DireccionEmpleadoConfiguration : IEntityTypeConfiguration<DireccionEmpleado>
    {
        public void Configure(EntityTypeBuilder<DireccionEmpleado> builder)
        {
            builder.ToTable("DireccionEmpleado");
            builder.Property(p=>p.Calle).HasColumnName("CalleEmpleado").HasMaxLength(250).IsRequired();
            builder.Property(p=>p.Carrera).HasColumnName("CarreraEmpleado").HasMaxLength(250).IsRequired();
            builder.Property(p=>p.Detalles).HasColumnName("DetallesEmpleado").HasMaxLength(250).IsRequired();
            builder.HasOne(p=>p.Barrio).WithMany(p=>p.DireccionEmpleados).HasForeignKey(p=>p.IdBarriofk);
        }
    }
}