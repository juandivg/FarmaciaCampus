using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Views;

namespace Domain.Interfaces;
public interface IProductoRepository : IGenericRepository<Producto>
{
    Task<IEnumerable<Producto>> GetProductosStock50(int cantidad);
    Task<IEnumerable<ProveedoresxProducto>> GetProveedoresxProductos();
    Task<IEnumerable<Producto>> GetProductosxProveedor(string proveedor);

}
