using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.ToTable("Departamento");
            builder.Property(p=>p.Nombre).HasColumnName("NombreDepartamento").HasMaxLength(250).IsRequired();
            builder.HasOne(p=>p.Pais).WithMany(p=>p.Departamentos).HasForeignKey(p=>p.IdPaisfk);
        }
    }
}