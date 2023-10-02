using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository
{
    public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleadoRepository
    {
        private readonly FarmaciaCampusContext _context;
        public EmpleadoRepository(FarmaciaCampusContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Empleado>> GetEmpleadosSinVentas(DateTime fechaInicio, DateTime fechaFinal)

        {
            return await (
                from emp in _context.Empleados
                where !_context.Ventas.Any(v => v.IdEmpleadofk == emp.Id && v.Fecha >= fechaInicio && v.Fecha <= fechaFinal)
                select new Empleado
                {
                    Id = emp.Id,
                    Cedula = emp.Cedula,
                    Correo = emp.Correo,
                    IdCargofk = emp.IdCargofk,
                    IdDireccionEmpfk = emp.IdDireccionEmpfk,
                    NombreEmpleado = emp.NombreEmpleado
                }
            ).ToListAsync();
        }

        public async Task<IEnumerable<EmpleadosxMenosCantidadVentas>> GetEmpleadosConMenosVentas(DateTime fechaInicio, DateTime fechaFinal, int cantidad)
        {
            return await (
                from emp in _context.Empleados
                join v in _context.Ventas on emp.Id equals v.IdEmpleadofk
                where v.Fecha.Year == 2023
                group emp by new { emp.Id, emp.NombreEmpleado } into g
                where g.Count() < cantidad
                select new EmpleadosxMenosCantidadVentas
                {
                    Id = g.Key.Id,
                    NombreEmpleado = g.Key.NombreEmpleado,
                    CantidadVentas = g.Count()
                }
            ).ToListAsync();
        }
        public async Task<IEnumerable<Empleado>> GetEmpleadoConMasProductos(int anio)
        {
            return await (
                from emp in _context.Empleados
                join v in _context.Ventas on emp.Id equals v.IdEmpleadofk
                join pv in _context.ProductoVentas on v.Id equals pv.IdVentafk
                join p in _context.Productos on pv.IdProductofk equals p.Id
                where v.Fecha.Year == anio
                group p by emp into g
                orderby g.Select(p => p.Id).Distinct().Count() descending
                select new Empleado
                {
                    Id = g.Key.Id,
                    NombreEmpleado = g.Key.NombreEmpleado,
                    Cedula = g.Key.Cedula,
                    Correo = g.Key.Correo,
                    IdCargofk = g.Key.IdCargofk,
                    IdDireccionEmpfk = g.Key.IdDireccionEmpfk
                }
            ).Take(1).ToListAsync();
        }
        public override async Task<IEnumerable<Empleado>> GetAllAsync()
        {
            return await _context.Empleados
                            .Include(p => p.IdDireccionEmpfk)
                            .Include(p => p.IdCargofk)
                            .ToListAsync();
        }
        public override async Task<Empleado> GetByIdAsync(int id)
        {
            return await _context.Empleados
                            .Include(p => p.IdDireccionEmpfk)
                            .Include(p => p.IdCargofk)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}