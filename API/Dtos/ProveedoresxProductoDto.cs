using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProveedoresxProductoDto
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public List<ProveedorDto> Proveedores { get; set; }
    }
}