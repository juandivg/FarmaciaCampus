using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Paciente");
            builder.Property(p=>p.NombrePaciente).HasColumnName("NombrePaciente").HasMaxLength(250).IsRequired();
            builder.Property(p=>p.cedula).HasColumnName("CedulaPaciente").HasMaxLength(250).IsRequired();
            builder.Property(p=>p.Correo).HasColumnName("CorreoPais").HasMaxLength(250).IsRequired();
            builder.HasOne(p=>p.DireccionPaciente).WithMany(p=>p.pacientes).HasForeignKey(p=>p.IdDireccionPac);


        }
    }
}