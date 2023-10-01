using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Views;

namespace Domain.Interfaces;
<<<<<<< HEAD
=======

>>>>>>> 9208e861474e91dfd173ae3e47577a24ef0734ac
public interface IEmpleadoRepository : IGenericRepository<Empleado>
{
    Task<IEnumerable<Empleado>> GetEmpleadosSinVentas(DateTime fechaInicio, DateTime fechaFinal);
    Task<IEnumerable<EmpleadosxMenosCantidadVentas>> GetEmpleadosConMenosVentas(DateTime fechaInicio, DateTime fechaFinal, int Cantidad);
<<<<<<< HEAD
=======
    Task<IEnumerable<Empleado>> GetEmpleadoConMasProductos(int anio);
>>>>>>> 9208e861474e91dfd173ae3e47577a24ef0734ac
}
