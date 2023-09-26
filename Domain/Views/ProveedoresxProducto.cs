using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Views
{
    public class ProveedoresxProducto
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public ICollection<Proveedor> Proveedores { get; set; }
    }
}