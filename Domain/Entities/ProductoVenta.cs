using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductoVenta:BaseEntity
    {
        public int IdVentafk { get; set; }
        public Venta Venta { get; set; }
        public int IdProductofk { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }
}