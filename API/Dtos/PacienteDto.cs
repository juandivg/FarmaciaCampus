using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class PacienteDto
{
    public int Id { get; set; }
    public string NombrePaciente { get; set; }
    public string cedula { get; set; }
    public string Correo { get; set; }
    public int IdDireccionPac { get; set; }
}
