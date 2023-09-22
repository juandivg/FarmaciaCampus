using System.Collections.Generic;
namespace Domain.Entities;

    public class Empleado : BaseEntity
    {
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public int IdCargofk { get; set; }
        public Cargo Cargo { get; set; }
        public int IdDireccionEmpfk { get; set; }
        public DireccionEmpleado DireccionEmp { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<TelefonoEmpleado> TelefonoEmpleados { get; set; }
        public ICollection<Receta> Recetas { get; set; }
        public ICollection<Venta> Ventas { get; set; }
    }
