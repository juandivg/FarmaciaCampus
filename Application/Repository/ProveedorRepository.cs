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

    public async Task<IEnumerable<Proveedor>> GetProveedoresSinVentas(DateTime fechaInicio, DateTime fechaFinal)
    {
        return await (from prov in _context.Proveedores
                      join pp in _context.ProveedorProductos on prov.Id equals pp.IdProveedorfk
                      join p in _context.Productos on pp.IdProductofk equals p.Id
                      join pv in _context.ProductoVentas on p.Id equals pv.IdProductofk into ventas
                      from venta in ventas.DefaultIfEmpty()
                      join v in _context.Ventas on venta.IdVentafk equals v.Id into ventasFecha
                      from ventaFecha in ventasFecha.DefaultIfEmpty()
                      group ventaFecha by prov into grupo
                      where grupo.Sum(vf => vf.Fecha >= fechaInicio && vf.Fecha <= fechaFinal ? 1 : 0) == 0
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

    public async Task<IEnumerable<ProveedoresConMasProductos>> GetProveedoresConMasProductos(DateTime fechaInicio, DateTime fechaFinal)
    {
        var proveedoresConMasProductos = await (
        from prov in _context.Proveedores
        join pv in _context.ProveedorProductos on prov.Id equals pv.IdProveedorfk
        join p in _context.Productos on pv.IdProductofk equals p.Id
        join pc in _context.ProductoCompras on p.Id equals pc.IdProductofk
        join c in _context.Compras on pc.IdComprafk equals c.Id
        where c.FechaCompra >= new DateTime(2023, 1, 1) && c.FechaCompra <= new DateTime(2023, 12, 31)
        group new { prov, pc } by prov into g
        select new ProveedoresConMasProductos
        {

            NombreProveedor = g.Key.NombreProveedor,
            TotalCantidad = g.Sum(x => x.pc.Cantidad)
        }
    ).ToListAsync();

        var maxTotalCantidad = proveedoresConMasProductos.First().TotalCantidad;

        var proveedoresConMasProductosFinal = proveedoresConMasProductos
            .Where(p => p.TotalCantidad == maxTotalCantidad)
            .Select(p => new ProveedoresConMasProductos
            {
                NombreProveedor = p.NombreProveedor,
                TotalCantidad = p.TotalCantidad

            }
            ).ToList();
        return proveedoresConMasProductosFinal;
    }
    public async Task<TotalProveedoresSuministraron> GetTotalProveedoresSuministraron(DateTime fechaInicio, DateTime fechaFinal)
    {
        var proveedores = await (
            from pp in _context.ProveedorProductos
            join p in _context.Productos on pp.IdProductofk equals p.Id
            join pc in _context.ProductoCompras on p.Id equals pc.IdProductofk
            join c in _context.Compras on pc.IdComprafk equals c.Id
            where c.FechaCompra >= fechaInicio && c.FechaCompra <= fechaFinal
            select pp.IdProveedorfk
        ).Distinct().ToListAsync();

        return new TotalProveedoresSuministraron { CantidadProveedores = proveedores.Count() };
    }
    public async Task<IEnumerable<Proveedor>> GetProveedoresxMenosMedicamentos(int cantidad)
    {
        return await (
            from prov in _context.Proveedores
            join pp in _context.ProveedorProductos on prov.Id equals pp.IdProveedorfk
            join p in _context.Productos on pp.IdProductofk equals p.Id
            where p.Stock < cantidad
            group prov by prov into g
            select new Proveedor
            {
                Id = g.Key.Id,
                NombreProveedor = g.Key.NombreProveedor,
                Correo = g.Key.Correo
            }
        ).ToListAsync();
    }
    public async Task<IEnumerable<ProveedoresConMasProductos>> GetProveedoresxProductos(int cantidad)
    {

        var proveedoresCantidad = await (from prov in _context.Proveedores
                                         join pp in _context.ProveedorProductos on prov.Id equals pp.IdProveedorfk
                                         join p in _context.Productos on pp.IdProductofk equals p.Id
                                         group p by prov into g
                                         // let cantidadProductos = g.Select(p => p.Id).Distinct().Count()
                                         //where cantidadProductos>=cantidad
                                         select new ProveedoresConMasProductos
                                         {
                                             NombreProveedor = g.Key.NombreProveedor,
                                             TotalCantidad = g.Count()
                                         }
    ).ToListAsync();
        var proveedores = proveedoresCantidad.Where(pc => pc.TotalCantidad >= cantidad)
        .Select(p => new ProveedoresConMasProductos
        {
            NombreProveedor = p.NombreProveedor,
            TotalCantidad = p.TotalCantidad

        }).ToList();
        return proveedores;
    }
    public override async Task<IEnumerable<Proveedor>> GetAllAsync()
    {
        return await _context.Proveedores
                        .Include(p => p.DireccionPro)
                        .ToListAsync();
    }
    public override async Task<Proveedor> GetByIdAsync(int id)
    {
        return await _context.Proveedores
                        .Include(p => p.DireccionPro)
                        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
