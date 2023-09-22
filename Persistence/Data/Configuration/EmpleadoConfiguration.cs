using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
{
    public void Configure(EntityTypeBuilder<Empleado> builder)
    {
        builder.ToTable("Empleado");

        builder.Property(p => p.Cedula).HasColumnName("Cedula").HasMaxLength(20).IsRequired();
        builder.Property(p => p.Correo).HasColumnName("Email").HasMaxLength(255).IsRequired();
        builder.HasOne(p => p.Cargo).WithMany(p => p.Empleados).HasForeignKey(p => p.IdCargofk);
        builder.HasOne(p => p.DireccionEmp).WithMany(p => p.Empleados).HasForeignKey(p => p.IdDireccionEmpfk);
    }
}
