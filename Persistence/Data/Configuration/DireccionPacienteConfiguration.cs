using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class DireccionPacienteConfiguration : IEntityTypeConfiguration<DireccionPaciente>
    {
        public void Configure(EntityTypeBuilder<DireccionPaciente> builder)
        {
                        builder.ToTable("DireccionPaciente");
            builder.Property(p=>p.Calle).HasColumnName("CallePaciente").HasMaxLength(250).IsRequired();
            builder.Property(p=>p.Carrera).HasColumnName("CarreraPaciente").HasMaxLength(250).IsRequired();
            builder.Property(p=>p.Detalles).HasColumnName("DetallesPaciente").HasMaxLength(250).IsRequired();
            builder.HasOne(p=>p.Barrio).WithMany(p=>p.DireccionPacientes).HasForeignKey(p=>p.IdBarriofk);
        }
    }
}