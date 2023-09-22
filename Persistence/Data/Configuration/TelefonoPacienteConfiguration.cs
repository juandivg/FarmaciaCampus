using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class TelefonoPacienteConfiguration : IEntityTypeConfiguration<TelefonoPaciente>
    {
        public void Configure(EntityTypeBuilder<TelefonoPaciente> builder)
        {
            builder.ToTable("TelefonoPaciente");
             builder.Property(p=>p.Numero).HasColumnName("Numero").HasMaxLength(250).IsRequired();
             builder.HasOne(p=>p.TipoTelefono).WithMany(p=>p.TelefonoPacientes).HasForeignKey(p=>p.IdTipoTelefonofk);
             builder.HasOne(p=>p.Paciente).WithMany(p=>p.TelefonoPacientes).HasForeignKey(p=>p.IdPacientefk);
        }
    }
}