using System.Collections.Generic;
namespace Domain.Entities;
    public class Usuario : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdEmpleadofk { get; set; }
        public Empleado Empleado { get; set; }
        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
