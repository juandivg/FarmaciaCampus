using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
    {
        public void Configure(EntityTypeBuilder<Ciudad> builder)
        {
            builder.ToTable("Ciudad");
            builder.Property(p=>p.Nombre).HasColumnName("NombreCiudad").HasMaxLength(250).IsRequired();
            builder.HasOne(p=>p.Departamento).WithMany(p=>p.Ciudades).HasForeignKey(p=>p.IdDepartamentofk);
        }
    }
}