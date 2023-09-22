using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class TelefonoProveedor : BaseEntity
    {
        public string Numero { get; set; }
        public int IdTipoTelefonofk { get; set; }
        public TipoTelefono TipoTelefono { get; set; }
        public int IdProveedorfk { get; set; }
        public Proveedor Proveedor { get; set; }
    }
