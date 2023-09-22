namespace Domain.Entities;
    public class UsuarioRol
    {
        public int IdUsuariofk { get; set; }
        public Usuario Usuario { get; set; }
        public int IdRolfk { get; set; }
        public Rol Rol { get; set; }
    }
