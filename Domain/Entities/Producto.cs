using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class Producto : BaseEntity
    {
        public string NombreProducto { get; set; }
        public int Stock { get; set; }
        public decimal PrecioC { get; set; }
        public decimal PrecioV { get; set; }
        public int IdTipoProductofk { get; set; }
        public TipoProducto TipoProducto { get; set; }
        public ICollection<ProveedorProducto> ProveedorProductos { get; set; }
        public ICollection<ProductoCompra> ProductoCompras { get; set; }
        public ICollection<ProductoVenta> ProductoVentas { get; set; }
        public ICollection<ProductoReceta> ProductoRecetas { get; set; }
    }
