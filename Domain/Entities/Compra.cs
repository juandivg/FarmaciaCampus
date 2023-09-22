using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class Compra : BaseEntity
    {
       public DateTime FechaCompra { get; set; }
       public ICollection<ProductoCompra> productoCompras { get; set; } 
    }
