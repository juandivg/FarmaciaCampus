using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class ProveedorRepository : GenericRepository<Proveedor>, IProveedorRepository

{
    private readonly FarmaciaCampusContext _context;
    public ProveedorRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CantidadVentasxProveedor>> GetCantidadVentasxProveedors()
    {
        return await (
            from prov in _context.Proveedores
            join pp in _context.ProveedorProductos on prov.Id equals pp.IdProveedorfk
            join pro in _context.Productos on pp.IdProductofk equals pro.Id
            join pv in _context.ProductoVentas on pro.Id equals pv.IdProductofk
            join ven in _context.Ventas on pv.IdVentafk equals ven.Id
            group pv by new { prov.NombreProveedor } into grp
            select new CantidadVentasxProveedor
            {
                NombreProveedor = grp.Key.NombreProveedor,
                Cantidad = grp.Sum(pv => pv.Cantidad)
            }
                        ).ToListAsync();
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

    public async Task<IEnumerable<TotalProductosxProveedor>> GetTotalProductosxProveedor()
    {
        return await (from prov in _context.Proveedores
                      join pp in _context.ProveedorProductos on prov.Id equals pp.IdProveedorfk
                      join pro in _context.Productos on pp.IdProductofk equals pro.Id
                      group pro by new { prov.NombreProveedor } into grp
                      select new TotalProductosxProveedor
                      {
                          NombreProveedor = grp.Key.NombreProveedor,
                          CantidadProductos = grp.Count()
                      }

        ).ToListAsync();
    }

    public async Task<IEnumerable<Proveedor>> GetProveedoresSinVentas(DateTime fechaVenta)
    {
        return await (from prov in _context.Proveedores
                      join pp in _context.ProveedorProductos on prov.Id equals pp.IdProveedorfk
                      join p in _context.Productos on pp.IdProductofk equals p.Id
                      join pv in _context.ProductoVentas on p.Id equals pv.IdProductofk into ventas
                      from venta in ventas.DefaultIfEmpty()
                      join v in _context.Ventas on venta.IdVentafk equals v.Id into ventasFecha
                      from ventaFecha in ventasFecha.DefaultIfEmpty()
                      group ventaFecha by prov into grupo
                      where grupo.Sum(vf => vf.Fecha > fechaVenta ? 1 : 0) == 0
                      select new Proveedor
                      {
                          Id = grupo.Key.Id,
                          NombreProveedor = grupo.Key.NombreProveedor,
                          Correo = grupo.Key.Correo

                      }).ToListAsync();
    }
    public async Task<IEnumerable<GananciaTotalxProveedor>> GetGananciaTotalxProveedor(DateTime fechaInicio, DateTime FechaFinal)
    {
        return await (
            from prov in _context.Proveedores
            join pp in _context.ProveedorProductos on prov.Id equals pp.IdProveedorfk
            join p in _context.Productos on pp.IdProductofk equals p.Id
            join pv in _context.ProductoVentas on p.Id equals pv.IdProductofk
            join v in _context.Ventas on pv.IdVentafk equals v.Id
            where v.Fecha >= fechaInicio && v.Fecha <= FechaFinal
            group new { prov, p, pv } by prov.NombreProveedor into grupo
            select new GananciaTotalxProveedor
            {
                NombreProveedor = grupo.Key,
                GananciaTotal = grupo.Sum(item => (item.p.PrecioV - item.p.PrecioC) * item.pv.Cantidad)
            }
        ).ToListAsync();
    }
}
