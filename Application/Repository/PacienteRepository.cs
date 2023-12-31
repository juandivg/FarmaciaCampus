using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository
{
    public class PacienteRepository : GenericRepository<Paciente>, IPacienteRepository
    {
        private readonly FarmaciaCampusContext _context;
        public PacienteRepository(FarmaciaCampusContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Paciente>> GetPacientesCompraron(string producto)
        {
            return await (
                from pac in _context.Pacientes
                join v in _context.Ventas on pac.Id equals v.IdPacientefk
                join pv in _context.ProductoVentas on v.Id equals pv.IdVentafk
                join pro in _context.Productos on pv.IdProductofk equals pro.Id
                where pro.NombreProducto == producto
                select new Paciente
                {
                    Id = pac.Id,
                    NombrePaciente = pac.NombrePaciente,
                    cedula = pac.cedula,
                    Correo = pac.Correo,
                    IdDireccionPac = pac.IdDireccionPac

                }
            ).ToListAsync();
        }

        public async Task<IEnumerable<PacientesMasGastaron>> GetPacientesMasGastaron(DateTime fechaInicio, DateTime fechaFinal)
        {

            var resultados = await (from pac in _context.Pacientes
                                    join v in _context.Ventas on pac.Id equals v.IdPacientefk
                                    join pv in _context.ProductoVentas on v.Id equals pv.IdVentafk
                                    join p in _context.Productos on pv.IdProductofk equals p.Id
                                    where v.Fecha >= fechaInicio && v.Fecha <= fechaFinal
                                    group new { pac, p, pv } by new { pac.Id, pac.NombrePaciente, pac.cedula } into grupo
                                    select new PacientesMasGastaron
                                    {
                                        NombrePaciente = grupo.Key.NombrePaciente,
                                        cedula = grupo.Key.cedula,
                                        TotalGastado = grupo.Sum(item => item.p.PrecioV * item.pv.Cantidad)
                                    }).ToListAsync();
            if (resultados.Any())
            {
                var maxGanancia = resultados.Max(r => r.TotalGastado);
                return resultados.Where(r => r.TotalGastado == maxGanancia);
            }
            else
            {
                return new List<PacientesMasGastaron>();
            }
        }
        public async Task<IEnumerable<Paciente>> GetPacientesxProducto(DateTime fechaInicio, DateTime fechaFinal, string producto)
        {
            return await (
                from pac in _context.Pacientes
                join v in _context.Ventas on pac.Id equals v.IdPacientefk
                join pv in _context.ProductoVentas on v.Id equals pv.IdVentafk
                join p in _context.Productos on pv.IdProductofk equals p.Id
                where p.NombreProducto == producto &&
                      v.Fecha >= fechaInicio &&
                      v.Fecha <= fechaFinal
                select new Paciente
                {
                    Id = pac.Id,
                    NombrePaciente = pac.NombrePaciente,
                    cedula = pac.cedula,
                    Correo = pac.Correo,
                    IdDireccionPac = pac.IdDireccionPac
                }
            ).ToListAsync();
        }
        public async Task<IEnumerable<Paciente>> GetPacientesNoCompraron(DateTime fechaInicio, DateTime fechaFinal)
        {
            return await (
                from paciente in _context.Pacientes
                where !(
                    from venta in _context.Ventas
                    join productoVenta in _context.ProductoVentas on venta.Id equals productoVenta.IdVentafk
                    where venta.Fecha.Year == 2023 && venta.IdPacientefk == paciente.Id
                    select venta.Id
                ).Any()
                select new Paciente
                {
                    Id = paciente.Id,
                    NombrePaciente = paciente.NombrePaciente,
                    cedula = paciente.cedula,
                    Correo = paciente.Correo,
                    IdDireccionPac = paciente.IdDireccionPac
                }
            ).ToListAsync();
        }
        public async Task<IEnumerable<PacientesMasGastaron>> GetTotalGastadoPaciente(DateTime fechaInicio, DateTime fechaFinal)
        {

            var resultados = await (from pac in _context.Pacientes
                                    join v in _context.Ventas on pac.Id equals v.IdPacientefk
                                    join pv in _context.ProductoVentas on v.Id equals pv.IdVentafk
                                    join p in _context.Productos on pv.IdProductofk equals p.Id
                                    where v.Fecha >= fechaInicio && v.Fecha <= fechaFinal
                                    group new { pac, p, pv } by new { pac.Id, pac.NombrePaciente, pac.cedula } into grupo
                                    select new PacientesMasGastaron
                                    {
                                        NombrePaciente = grupo.Key.NombrePaciente,
                                        cedula = grupo.Key.cedula,
                                        TotalGastado = grupo.Sum(item => item.p.PrecioV * item.pv.Cantidad)
                                    }).ToListAsync();
            return resultados;
        }

        public override async Task<IEnumerable<Paciente>> GetAllAsync()
        {
            return await _context.Pacientes
                            .Include(p => p.DireccionPaciente)
                            .ToListAsync();
        }
        public override async Task<Paciente> GetByIdAsync(int id)
        {
            return await _context.Pacientes
                            .Include(p => p.DireccionPaciente)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }
            public override async Task<(int totalRegistros, IEnumerable<Paciente> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Pacientes as IQueryable<Paciente>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombrePaciente.ToLower().Contains(search));
        }
        
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 .Include(u => u.DireccionPaciente)
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
    }
}