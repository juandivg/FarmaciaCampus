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
    public class CompraRepository : GenericRepository<Compra>, ICompraRepository
    {
        private readonly FarmaciaCampusContext _context;
        public CompraRepository(FarmaciaCampusContext context) : base(context)
        {
            _context = context;
        }
        // public override async Task<IEnumerable<Compra>> GetAllAsync()
        // {
        //     return await _context.Compras
        //     .Include(p=>p.productoCompras)
        //     .ThenInclude(p =>p.Producto)
        //     .ThenInclude(p=>p.ProveedorProductos)
        //     .ThenInclude(p=>p.Proveedor).Where(p=> p.)
        //     .ToListAsync();
        // }
    }
}