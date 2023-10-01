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
    Task<IEnumerable<Paciente>> GetPacientesxProducto(DateTime fechaInicio, DateTime fechaFinal, string producto);
    Task<IEnumerable<Paciente>> GetPacientesNoCompraron(DateTime fechaInicio, DateTime fechaFinal);
    Task<IEnumerable<PacientesMasGastaron>> GetTotalGastadoPaciente(DateTime  fechaInicio, DateTime fechaFinal);
}
