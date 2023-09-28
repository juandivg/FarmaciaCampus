using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
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
    }
}