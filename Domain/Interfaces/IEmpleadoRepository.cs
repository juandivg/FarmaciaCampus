using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;
public interface IEmpleadoRepository : IGenericRepository<Empleado>
{
    Task<IEnumerable<Empleado>> GetEmpleadosSinVentas(DateTime fechaInicio, DateTime fechaFinal);
}
