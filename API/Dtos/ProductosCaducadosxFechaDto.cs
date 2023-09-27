using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class ProductosCaducadosxFechaDto
{
    public string NombreProducto { get; set; }
    public DateTime FechaVencimiento { get; set; }
}
