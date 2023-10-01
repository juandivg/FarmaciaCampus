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
    Task<IEnumerable<CantidadVentasxProveedor>> GetCantidadVentasxProveedors(); ///
    Task<IEnumerable<TotalProductosxProveedor>> GetTotalProductosxProveedor();

    Task<IEnumerable<Proveedor>> GetProveedoresSinVentas(DateTime fechaInicio, DateTime fechaFinal);
    Task<IEnumerable<GananciaTotalxProveedor>> GetGananciaTotalxProveedor(DateTime fechaInicio, DateTime FechaFinal);
    Task<IEnumerable<ProveedoresConMasProductos>> GetProveedoresConMasProductos(DateTime fechaInicio, DateTime fechaFinal);
    Task<TotalProveedoresSuministraron> GetTotalProveedoresSuministraron(DateTime fechaInicio, DateTime fechaFinal);
    Task<IEnumerable<Proveedor>> GetProveedoresxMenosMedicamentos(int cantidad);
<<<<<<< HEAD
=======
    Task<IEnumerable<ProveedoresConMasProductos>> GetProveedoresxProductos(int cantidad);
>>>>>>> 9208e861474e91dfd173ae3e47577a24ef0734ac
}
