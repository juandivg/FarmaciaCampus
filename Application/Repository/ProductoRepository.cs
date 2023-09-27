using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<IEnumerable<Producto>> GetProductosxProveedor(string proveedor)
    {
        return await (
            from prov in _context.Proveedores
            join pp in _context.ProveedorProductos on prov.Id equals pp.IdProveedorfk
            join pro in _context.Productos on pp.IdProductofk equals pro.Id
            join pc in _context.ProductoCompras on pro.Id equals pc.IdProductofk
            join com in _context.Compras on pc.IdComprafk equals com.Id 
            where prov.NombreProveedor==proveedor
            select new Producto
            {
                Id=pro.Id,
                NombreProducto=pro.NombreProducto
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

        
    
    }
