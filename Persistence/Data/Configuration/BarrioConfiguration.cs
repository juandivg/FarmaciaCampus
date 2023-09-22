using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class BarrioConfiguration : IEntityTypeConfiguration<Barrio>
    {
        public void Configure(EntityTypeBuilder<Barrio> builder)
        {
            builder.Property(p=>p.NombreBarrio).HasColumnName("NombreBarrio").HasMaxLength(250).IsRequired();
            builder.HasOne(p=>p.Ciudad).WithMany(p=>p.Barrios).HasForeignKey(p=>p.IdCiudadfk);

        }
    }
}