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
    public class ProveedorRepository : GenericRepository<Proveedor>, IProveedorRepository

    {
        private readonly FarmaciaCampusContext _context;
        public ProveedorRepository(FarmaciaCampusContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Proveedor>> GetProveedoresSinCompras()
        {

            DateTime haceUnAnio = DateTime.Now.AddYears(-1);
            return await _context.Proveedores
            .Join(_context.ProveedorProductos, prov => prov.Id, pp => pp.IdProveedorfk, (prov, pp) => new { Proveedor = prov, ProveedorProducto = pp })
            .Join(_context.Productos, x => x.ProveedorProducto.IdProductofk, pro => pro.Id, (x, pro) => new { Proveedor = x.Proveedor, Producto = pro })
            .Join(_context.ProductoCompras, x => x.Producto.Id, pc => pc.IdProductofk, (x, pc) => new { Proveedor = x.Proveedor, Producto = x.Producto, ProductoCompra = pc })
            .Join(_context.Compras, x => x.ProductoCompra.IdComprafk, com => com.Id, (x, com) => new { Proveedor = x.Proveedor, Producto = x.Producto, ProductoCompra = x.ProductoCompra, Compra = com })
            .GroupBy(x => x.Proveedor)
            .Select(grp => new
            {
                Proveedor = grp.Key,
                UltimaCompra = grp.Max(x => x.Compra.FechaCompra)
            })
            .Where(x => x.UltimaCompra < haceUnAnio)
            .Select(x => x.Proveedor).ToListAsync();
        }
    }
}