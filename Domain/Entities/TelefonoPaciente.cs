using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TelefonoPaciente:BaseEntity
    {
        public string Numero { get; set; }
        public int IdTipoTelefonofk { get; set; }
        public TipoTelefono TipoTelefono { get; set; }
        public int IdPacientefk { get; set; }
        public Paciente Paciente { get; set; }
    }
}