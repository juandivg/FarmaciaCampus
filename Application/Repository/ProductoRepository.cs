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
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        private readonly FarmaciaCampusContext _context;
        public ProductoRepository(FarmaciaCampusContext context) : base(context)
        {
            _context = context;
        }
        // public override async Task<IEnumerable<Producto>> GetAllAsync()
        // {
        //     return await _context.Productos
        //         .Include(p=>p.ProveedorProductos.Select(p=>p.))
        //         .ThenInclude(p=>p.Proveedor)
        //         .ToListAsync();
        // }
        public async Task<IEnumerable<Producto>> GetProductosStock50(int cantidad)
        {
            return await _context.Productos
            .Where(p => p.Stock < cantidad)
            .ToListAsync();
        }

        public async Task<IEnumerable<ProveedoresxProducto>> GetProveedoresxProductos()
        {
            return await _context.Productos
            .GroupJoin(
            _context.ProveedorProductos,
            producto => producto.Id,
            pp => pp.IdProductofk,
            (producto, pp) => new ProveedoresxProducto
            {
                Id = producto.Id,
                NombreProducto = producto.NombreProducto,
                Proveedores = pp.Join(
                    _context.Proveedores,
                    pp => pp.IdProveedorfk,
                    proveedor => proveedor.Id,
                    (pp, proveedor) => new Proveedor
                    {
                        Id = proveedor.Id,
                        NombreProveedor = proveedor.NombreProveedor,
                        Correo = proveedor.Correo
                    }
                ).ToList()
            }).ToListAsync();
        }
    }
}