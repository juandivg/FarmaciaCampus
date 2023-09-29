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
        public async Task<IEnumerable<Paciente>> GetPacientesNoCompraron(string producto)
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

        public async Task<IEnumerable<PacientesMasGastaron>> GetPacientesMasGastaron(int anio)
        {
            DateTime fechaInicio = new DateTime(anio, 1, 1);
    DateTime fechaFinal = new DateTime(anio, 12, 31);

    var resultados = await (from pac in _context.Pacientes
                            join v in _context.Ventas on pac.Id equals v.IdPacientefk
                            join pv in _context.ProductoVentas on v.Id equals pv.IdVentafk
                            join p in _context.Productos on pv.IdProductofk equals p.Id
                            where v.Fecha >= fechaInicio && v.Fecha <= fechaFinal
                            group new { pac, p, pv } by new { pac.Id, pac.NombrePaciente, pac.cedula} into grupo
                            select new PacientesMasGastaron
                            {
                                NombrePaciente = grupo.Key.NombrePaciente,
                                cedula=grupo.Key.cedula,
                                TotalGastado = grupo.Sum(item => item.p.PrecioV * item.pv.Cantidad)
                            }).ToListAsync();

    var maxGanancia = resultados.Max(r => r.TotalGastado);

    return resultados.Where(r => r.TotalGastado == maxGanancia);
            // .Where(r => r.TotalGastado == maxGanancia)
            //                  .Select(r => new PacientesMasGastaron
            //                  {
            //                      NombrePaciente = r.NombrePaciente
            //                  });
            //     DateTime fechaInicio =new DateTime (anio,1,1);
            //     DateTime fechaFinal=new DateTime (anio,12,31);
            //  var maxGanancia=await (from pac in _context.Pacientes
            //                      join v in _context.Ventas on pac.Id equals v.IdPacientefk
            //                      join pv in _context.ProductoVentas on v.Id equals pv.IdVentafk
            //                      join p in _context.Productos on pv.IdProductofk equals p.Id
            //                      where v.Fecha >= fechaInicio && v.Fecha <= fechaFinal
            //                      group new {pac, p, pv } by new { pac.Id, pac.NombrePaciente } into grupo
            //                      select grupo.Sum(item => item.p.PrecioV * item.pv.Cantidad))
            //                      .MaxAsync();

            // return await (from pac in _context.Pacientes
            //                         join v in _context.Ventas on pac.Id equals v.IdPacientefk
            //                         join pv in _context.ProductoVentas on v.Id equals pv.IdVentafk
            //                         join p in _context.Productos on pv.IdProductofk equals p.Id
            //                         where v.Fecha >= fechaInicio && v.Fecha <= fechaFinal
            //                         group new {pac, p, pv } by new { pac.Id, pac.NombrePaciente } into grupo
            //                         let totalGastado = grupo.Sum(item => item.p.PrecioV * item.pv.Cantidad)
            //                         where totalGastado == maxGanancia
            //                         select new PacientesMasGastaron
            //                         {
            //                             NombrePaciente = grupo.Key.NombrePaciente
            //                         }).ToListAsync();
        }
    }
}