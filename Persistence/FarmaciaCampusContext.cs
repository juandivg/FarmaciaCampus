using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;
public class FarmaciaCampusContext : DbContext
{
    public FarmaciaCampusContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Empleado>()
        .HasOne<Usuario>(p=>p.Usuario)
        .WithOne(ad => ad.Empleado)
        .HasForeignKey<Usuario>(ad => ad.IdEmpleadofk);
        modelBuilder.Entity<Receta>()
        .HasOne<Venta>(p=>p.Venta)
        .WithOne(p=>p.Receta)
        .HasForeignKey<Venta>(p=>p.IdRecetafk);
    }
}
