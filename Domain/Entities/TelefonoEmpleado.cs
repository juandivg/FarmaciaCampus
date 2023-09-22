using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class TelefonoEmpleado:BaseEntity
    {
        public string Numero { get; set; }
        public int IdTipoTelefonofk { get; set; }
        public TipoTelefono TipoTelefono { get; set; }
        public int IdEmpleadofk { get; set; }
        public Empleado Empleado { get; set; }
    }
