using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Barrio:BaseEntity
    {
        public string NombreBarrio { get; set; }
        public int IdCiudadfk { get; set; }
        public Ciudad Ciudad { get; set; }
        public ICollection <DireccionProveedor> DireccionProveedores { get; set; }
        public ICollection<DireccionEmpleado> DireccionEmpleados { get; set; }
        public ICollection<DireccionPaciente> DireccionPacientes { get; set; }
        

    }
}