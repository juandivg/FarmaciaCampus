using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class TelefonoEmpleadoConfiguration : IEntityTypeConfiguration<TelefonoEmpleado>
    {
        public void Configure(EntityTypeBuilder<TelefonoEmpleado> builder)
        {
             builder.ToTable("TelefonoPaciente");
             builder.Property(p=>p.Numero).HasColumnName("Numero").HasMaxLength(250).IsRequired();
             builder.HasOne(p=>p.TipoTelefono).WithMany(p=>p.TelefonoEmpleados).HasForeignKey(p=>p.IdTipoTelefonofk);
             builder.HasOne(p=>p.Empleado).WithMany(p=>p.TelefonoEmpleados).HasForeignKey(p=>p.IdEmpleadofk);
        }
    }
}