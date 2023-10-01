using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Views;

namespace Domain.Interfaces;
public interface IEmpleadoRepository : IGenericRepository<Empleado>
{
    Task<IEnumerable<Empleado>> GetEmpleadosSinVentas(DateTime fechaInicio, DateTime fechaFinal);
    Task<IEnumerable<EmpleadosxMenosCantidadVentas>> GetEmpleadosConMenosVentas(DateTime fechaInicio, DateTime fechaFinal, int Cantidad);
    Task<IEnumerable<Empleado>> GetEmpleadoConMasProductos(int anio);
}
