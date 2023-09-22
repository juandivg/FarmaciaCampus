using System.Collections.Generic;
namespace Domain.Entities;
    public class Usuario : BaseEntity
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdEmpleadofk { get; set; }
        public Empleado Empleado { get; set; }
        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
        public ICollection<Rol> Roles = new HashSet<Rol>();
        public ICollection<RefreshToken> RefreshTokens = new HashSet<RefreshToken>();
    }
