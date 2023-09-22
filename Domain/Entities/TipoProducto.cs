using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class TipoProducto : BaseEntity
    {
        public string TipoProducto { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }
