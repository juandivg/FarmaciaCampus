using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class DireccionProveedor:BaseEntity
    {
        public string Calle { get; set; }
        public string Carrera { get; set; }
        public string Detalles { get; set; }
        public int IdBarriofk { get; set; }
        public Barrio Barrio {get; set;}
        public ICollection<Proveedor> Proveedores { get; set; }
    }
