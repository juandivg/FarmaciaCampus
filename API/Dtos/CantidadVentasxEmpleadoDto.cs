using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class CantidadVentasxEmpleadoDto
{
    public int Id { get; set; }
    public string NombreEmpleado { get; set; }
    public int CantidadVentas { get; set; }
}
