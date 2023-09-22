using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Venta:BaseEntity
    {
        public DateTime Fecha { get; set; }
        public int IdPacientefk { get; set; }
        public Paciente Paciente { get; set; }
        public int IdEmpleadofk { get; set; }
        public Empleado Empleado { get; set; }
        public ICollection<ProductoVenta> ProductoVentas { get; set; }
    }
}