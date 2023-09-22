using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;
public class FarmaciaCampusContext : DbContext
{
    public FarmaciaCampusContext(DbContextOptions<FarmaciaCampusContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder){

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        modelBuilder.Entity<Empleado>()
        .HasOne<Usuario>(p=>p.Usuario)
        .WithOne(ad => ad.Empleado)
        .HasForeignKey<Usuario>(ad => ad.IdEmpleadofk);
        modelBuilder.Entity<Receta>()
        .HasOne<Venta>(p=>p.Venta)
        .WithOne(p=>p.Receta)
        .HasForeignKey<Venta>(p=>p.IdRecetafk);
    }
    public DbSet<Barrio> Barrios { get; set; }
    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<Ciudad> Ciudades { get; set; }
    public DbSet<Compra> Compras { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<DireccionEmpleado> DireccionEmpleados { get; set; }
    public DbSet<DireccionPaciente> DireccionPacientes { get; set; }
    public DbSet<DireccionProveedor> DireccionProveedores { get; set; }
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Pais> Paises { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<ProductoCompra> ProductoCompras { get; set; }
    public DbSet<ProductoReceta> ProductoRecetas { get; set; }
    public DbSet<ProductoVenta> ProductoVentas { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<ProveedorProducto> ProveedorProductos { get; set; }
    public DbSet<Receta> Recetas { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<TelefonoEmpleado> TelefonoEmpleados { get; set; }
    public DbSet<TelefonoPaciente> TelefonoPacientes { get; set; }
    public DbSet<TelefonoProveedor> TelefonoProveedores { get; set; }
    public DbSet<TipoProducto> TipoProductos { get; set; }
    public DbSet<TipoTelefono> TipoTelefonos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Venta> Ventas { get; set; }


}
