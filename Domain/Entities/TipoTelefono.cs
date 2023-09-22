using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class TipoTelefono : BaseEntity
    {
        public string TipoProducto { get; set; }
        public ICollection<TelefonoProveedor> TelefonoProveedores { get; set; }
        public ICollection<TelefonoEmpleado> TelefonoEmpleados { get; set; }
        public ICollection<TelefonoPaciente> TelefonoPacientes { get; set; }
    }
