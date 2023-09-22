using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class Receta : BaseEntity
    {
        public DateTime FechaReceta { get; set; }
        public string Detalle { get; set; }

        public int IdEmpleadofk { get; set; }
        public Empleado Empleado { get; set; }

        public int IdPacientefk { get; set; }
        public Paciente Paciente { get; set; }

        public ICollection<ProductoReceta> ProductoRecetas { get; set; }
        public ICollection<Venta> Ventas { get; set; }
    }
