using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class ProductoCompra : BaseEntity
    {
        public int IdProductofk { get; set; }
        public Producto Producto { get; set; }
        public int IdComprafk { get; set; }
        public Compra Compra { get; set; }
        public int Cantidad { get; set; }
    }
