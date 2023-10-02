using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class ProductoFullDto
{
    public int Id { get; set; }
    public string NombreProducto { get; set; }
    public int Stock { get; set; }
    public decimal PrecioC { get; set; }
    public decimal PrecioV { get; set; }
    public int IdTipoProductofk { get; set; }

}
