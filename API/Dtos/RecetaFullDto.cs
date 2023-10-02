using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class RecetaFullDto
{
    public int Id { get; set; }
    public DateTime FechaReceta { get; set; }
    public string Detalle { get; set; }
    public int IdEmpleadofk { get; set; }
    public int IdPacientefk { get; set; }
}
