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
public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
{
    private readonly FarmaciaCampusContext _context;
    public ProductoRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Producto>> GetProductosStock50(int cantidad)
    {
        return await _context.Productos
        .Where(p => p.Stock < cantidad)
        .ToListAsync();

    }

    public async Task<IEnumerable<Producto>> GetProductosxProveedor(string proveedor)
    {
        return await (
            from prov in _context.Proveedores
            join pp in _context.ProveedorProductos on prov.Id equals pp.IdProveedorfk
            join pro in _context.Productos on pp.IdProductofk equals pro.Id
            join pc in _context.ProductoCompras on pro.Id equals pc.IdProductofk
            join com in _context.Compras on pc.IdComprafk equals com.Id
            where prov.NombreProveedor == proveedor
            select new Producto
            {
                Id = pro.Id,
                NombreProducto = pro.NombreProducto
            }).ToListAsync();
    }

    public async Task<IEnumerable<ProveedoresxProducto>> GetProveedoresxProductos()
    {
        var proveedoresxProductos = await (
        from producto in _context.Productos
        join pp in _context.ProveedorProductos on producto.Id equals pp.IdProductofk into proveedorProductosGroup
        from provProducto in proveedorProductosGroup.DefaultIfEmpty()
        join proveedor in _context.Proveedores on provProducto.IdProveedorfk equals proveedor.Id into proveedoresGroup
        from prov in proveedoresGroup.DefaultIfEmpty()
        group prov by new { producto.Id, producto.NombreProducto } into grp
        select new ProveedoresxProducto
        {
            Id = grp.Key.Id,
            NombreProducto = grp.Key.NombreProducto,
            Proveedores = grp.Select(p => new Proveedor
            {
                Id = p.Id,
                NombreProveedor = p.NombreProveedor,
                Correo = p.Correo
            }).ToList()
        }
    ).ToListAsync();

        return proveedoresxProductos;

        // return await _context.Productos
        // .GroupJoin(
        // _context.ProveedorProductos,
        // producto => producto.Id,
        // pp => pp.IdProductofk,
        // (producto, pp) => new ProveedoresxProducto
        // {
        //     Id = producto.Id,
        //     NombreProducto = producto.NombreProducto,
        //     Proveedores = pp.Join(
        //         _context.Proveedores,
        //         pp => pp.IdProveedorfk,
        //         proveedor => proveedor.Id,
        //         (pp, proveedor) => new Proveedor
        //         {
        //             Id = proveedor.Id,
        //             NombreProveedor = proveedor.NombreProveedor,
        //             Correo = proveedor.Correo
        //         }
        //     ).ToList()
        // }).ToListAsync();
    }

    public async Task<IEnumerable<ProductosCaducadosxFecha>> GetProductosCaducadosAntes(DateTime fechaVencimiento)
    {
        return await (
            from p in _context.Productos
            join pp in _context.ProveedorProductos on p.Id equals pp.IdProductofk
            where pp.FechaVencimiento < fechaVencimiento
            select new ProductosCaducadosxFecha
            {
                NombreProducto = p.NombreProducto,
                FechaVencimiento = pp.FechaVencimiento
            }
        ).ToListAsync();
    }

    public async Task<IEnumerable<Producto>> GetProductosSinVender()
    {
        return await (
        from p in _context.Productos
        join pv in _context.ProductoVentas on p.Id equals pv.IdProductofk into pvGroup
        from pv in pvGroup.DefaultIfEmpty()
        join v in _context.Ventas on pv.IdVentafk equals v.Id into vGroup
        from v in vGroup.DefaultIfEmpty()
        where pv == null
        select new Producto
        {
            Id = p.Id,
            NombreProducto = p.NombreProducto,
            Stock = p.Stock,
            PrecioV = p.PrecioV,
            IdTipoProductofk = p.IdTipoProductofk
        }
        ).ToListAsync();
    }

    public async Task<IEnumerable<Producto>> GetProductosMasCaros()
    {
        var maxPrecioCompra = _context.Productos.Max(p => p.PrecioC);
        return await (
            _context.Productos.Where(p => p.PrecioC == maxPrecioCompra)
            .Select(p => new Producto
            {
                Id = p.Id,
                NombreProducto = p.NombreProducto,
                Stock = p.Stock,
                PrecioV = p.PrecioV,
                IdTipoProductofk = p.IdTipoProductofk


            })
        ).ToListAsync();
    }

    public async Task<TotalVentasxRango> GetMedicamentosEnRango(DateTime fechaInicio, DateTime fechaFinal)
    {
        var TotalRango = await (
            from p in _context.Productos
            join pv in _context.ProductoVentas on p.Id equals pv.IdProductofk
            join v in _context.Ventas on pv.IdVentafk equals v.Id
            where v.Fecha >= fechaInicio && v.Fecha <= fechaFinal
            select pv.Cantidad
        ).SumAsync();

        return new TotalVentasxRango
        {
            VentasTotales = TotalRango
        };
    }

    public async Task<IEnumerable<Producto>> GetMedicamentosMenosVendidos(DateTime fechaInicio, DateTime fechaFinal)
    {
        var minimo = _context.ProductoVentas.Min(p => p.Cantidad);
        return await (
            from p in _context.Productos
            join pv in _context.ProductoVentas on p.Id equals pv.IdProductofk
            join v in _context.Ventas on pv.IdVentafk equals v.Id
            where pv.Cantidad == minimo && v.Fecha >= fechaInicio && v.Fecha <= fechaFinal
            select new Producto
            {
                Id = p.Id,
                NombreProducto = p.NombreProducto,
                Stock = p.Stock,
                PrecioV = p.PrecioV,
                IdTipoProductofk = p.IdTipoProductofk
            }
        ).ToListAsync();
    }

    public async Task<IEnumerable<PromedioProductosxVenta>> GetPromedioProductosxVentas()
    {
        return await (
            from p in _context.Productos
            join pv in _context.ProductoVentas on p.Id equals pv.IdProductofk
            join v in _context.Ventas on pv.IdVentafk equals v.Id
            group pv by v.Id into grupo
            select new PromedioProductosxVenta
            {
                Id = grupo.Key,
                PromedioProductos = grupo.Sum(item => item.Cantidad) / grupo.Count(),
            }
        ).ToListAsync();
    }
    public async Task<IEnumerable<Producto>> GetProductosExpirados(DateTime fechaInicio, DateTime fechaFinal)
    {
        return await (
            from com in _context.Compras
            join pc in _context.ProductoCompras on com.Id equals pc.IdComprafk
            join p in _context.Productos on pc.IdProductofk equals p.Id
            join pp in _context.ProveedorProductos on p.Id equals pp.IdProductofk
            where pp.FechaVencimiento >= fechaInicio && pp.FechaVencimiento <= fechaFinal
            select new Producto
            {
                Id = p.Id,
                NombreProducto = p.NombreProducto,
                Stock = p.Stock,
                PrecioV = p.PrecioV,
                IdTipoProductofk = p.IdTipoProductofk
            }
        ).ToListAsync();
    }
    public async Task<IEnumerable<Producto>> GetProductosSinVenderFecha(DateTime fechaInicio, DateTime fechaFinal)
    {
        return await (
        from p in _context.Productos
        join pv in _context.ProductoVentas on p.Id equals pv.IdProductofk into pvGroup
        from pv in pvGroup.DefaultIfEmpty()
        join v in _context.Ventas on pv.IdVentafk equals v.Id into vGroup
        from v in vGroup.DefaultIfEmpty()
        where pv == null && v.Fecha >= fechaInicio && v.Fecha <= fechaFinal
        select new Producto
        {
            Id = p.Id,
            NombreProducto = p.NombreProducto,
            Stock = p.Stock,
            PrecioV = p.PrecioV,
            IdTipoProductofk = p.IdTipoProductofk
        }
        ).ToListAsync();
    }
    public async Task<IEnumerable<Producto>> GetProductosPrecioStock(int precio, int stock)
    {
        return await _context.Productos.Where(p => p.PrecioV > precio && p.Stock < stock).ToListAsync();
    }
    public override async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _context.Productos
                        .Include(p => p.IdTipoProductofk)
                        .ToListAsync();
    }
    public override async Task<Producto> GetByIdAsync(int id)
    {
        return await _context.Productos
                        .Include(p => p.IdTipoProductofk)
                        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
