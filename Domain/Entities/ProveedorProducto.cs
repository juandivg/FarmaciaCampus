using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class ProveedorProducto : BaseEntity
    {
        public DateTime FechaVencimiento { get; set; }
        public int IdProveedorfk { get; set; }
        public Proveedor Proveedor { get; set; }
        public int IdProductofk { get; set; }
        public Producto Producto { get; set; }
    }
