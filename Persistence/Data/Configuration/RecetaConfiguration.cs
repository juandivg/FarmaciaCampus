using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class RecetaConfiguration : IEntityTypeConfiguration<Receta>
{
    public void Configure(EntityTypeBuilder<Receta> builder)
    {
        builder.ToTable("receta");

        builder.Property(p => p.FechaReceta).HasColumnName("FechaReceta").IsRequired();
        builder.Property(p => p.Detalle).HasColumnName("Detalle").HasMaxLength(500).IsRequired();
        builder.HasOne(p => p.Empleado).WithMany(p => p.Recetas).HasForeignKey(p => p.IdEmpleadofk);
        builder.HasOne(p => p.Paciente).WithMany(p => p.Recetas).HasForeignKey(P => P.IdPacientefk);

    }
}
