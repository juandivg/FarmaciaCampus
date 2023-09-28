using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Views;

namespace Domain.Interfaces;
public interface IVentaRepository : IGenericRepository<Venta>
{
    Task<IEnumerable<VentasTotalesxProducto>> GetVentasxMedicamento(string medicamento);
    Task<TotalDineroVentas> GetTotalDineroVentas();
    Task<IEnumerable<CantidadVentasxEmpleado>> GetCantidadVentasxEmpleado(DateTime fechaInicio, DateTime fechaFinal);
    Task<IEnumerable<CantidadVentasxEmpleado>> GetCantidadVentasxEmpleadoNumero(int cantidad);

}
