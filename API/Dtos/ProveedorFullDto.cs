using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class ProveedorFullDto
{
    public int Id { get; set; }
    public string NombreProveedor { get; set; }
    public string NIT { get; set; }
    public string Correo { get; set; }
    public int IdDireccionProFK { get; set; }
}
