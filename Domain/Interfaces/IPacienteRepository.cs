using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Views;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;
public interface IPacienteRepository : IGenericRepository<Paciente>
{
    Task<IEnumerable<Paciente>> GetPacientesCompraron(string producto);
    Task<IEnumerable<PacientesMasGastaron>> GetPacientesMasGastaron(DateTime fechaInicio, DateTime fechaFinal);

}
