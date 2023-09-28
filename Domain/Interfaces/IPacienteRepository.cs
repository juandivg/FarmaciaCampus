using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;
public interface IPacienteRepository : IGenericRepository<Paciente>
{
    Task<IEnumerable<Paciente>> GetPacientesNoCompraron(string producto);
}
