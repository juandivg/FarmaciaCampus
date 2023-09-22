using System.Collections.Generic;
namespace Domain.Entities;
    public class Rol : BaseEntity
    {
        public string NombreRol { get; set; }
        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
        public ICollection<Usuario> Usuarios = new HashSet<Usuario>();
    }
