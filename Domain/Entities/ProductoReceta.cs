using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class ProductoReceta : BaseEntity
    {
        public int IdRecetafk { get; set; }
        public Receta Receta { get; set; }
        public int IdProductofk { get; set; }
        public Producto Producto { get; set; }
    }
