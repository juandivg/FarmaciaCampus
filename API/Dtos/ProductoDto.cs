using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProductoDto
    {
        public string NombreProducto { get; set; }
        public int Stock { get; set; }
        public decimal PrecioV { get; set; }
        public int IdTipoProductofk { get; set; }
        public List<ProveedorDto> Proveedores { get; set; }

    }
}