using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class EmpleadoDto
{
    public int Id { get; set; }
    public string NombreEmpleado { get; set; }
    public string Cedula { get; set; }
    public string Correo { get; set; }
    public int IdCargofk { get; set; }
    public int IdDireccionEmpfk { get; set; }
}
