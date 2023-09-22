using System.Collections.Generic;
namespace Domain.Entities;
    public class Proveedor : BaseEntity
    {
        public string NombreProveedor { get; set; }
        public string NIT { get; set; }
        public string Correo { get; set; }
        public int IdDireccionProFK { get; set; }
        public DireccionProveedor DireccionPro { get; set; }

        public ICollection<TelefonoProveedor> TelefonoProveedores { get; set; }
        public ICollection<ProveedorProducto> ProveedorProductos { get; set; }
    }
