using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PacientesMasGastaronDto
    {
        public string NombrePaciente { get; set; }
        public string cedula { get; set; }
        public decimal TotalGastado { get; set; }
    }
}