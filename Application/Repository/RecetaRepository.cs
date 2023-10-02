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
    public class RecetaRepository : GenericRepository<Receta>, IRecetaRepository
    {

        private readonly FarmaciaCampusContext _context;
        public RecetaRepository(FarmaciaCampusContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Receta>> GetRecetasFecha(DateTime fecha)
        {
            return await _context.Recetas.Where(p => p.FechaReceta >= fecha).ToListAsync();
        }
        public override async Task<IEnumerable<Receta>> GetAllAsync()
        {
            return await _context.Recetas
                            .Include(p => p.Empleado)
                            .Include(p => p.Paciente)
                            .ToListAsync();
        }
        public override async Task<Receta> GetByIdAsync(int id)
        {
            return await _context.Recetas
                            .Include(p => p.Empleado)
                            .Include(p => p.Paciente)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}