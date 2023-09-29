using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Views;

namespace Domain.Interfaces;
public interface IProveedorRepository : IGenericRepository<Proveedor>
{
    Task<IEnumerable<Proveedor>> GetProveedoresSinCompras();
    Task <IEnumerable<CantidadVentasxProveedor>> GetCantidadVentasxProveedors(); ///
    Task<IEnumerable<TotalProductosxProveedor>> GetTotalProductosxProveedor();

    Task<IEnumerable<Proveedor>> GetProveedoresSinVentas(DateTime fechaVenta);
    Task<IEnumerable<GananciaTotalxProveedor>> GetGananciaTotalxProveedor(DateTime fechaInicio, DateTime FechaFinal);
    Task<IEnumerable<ProveedoresConMasProductos>> GetProveedoresConMasProductos(DateTime fechaInicio, DateTime fechaFinal);
}
