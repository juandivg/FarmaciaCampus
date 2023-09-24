using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class PacienteRepository : GenericRepository<Paciente>, IPacienteRepository
    {
        private readonly FarmaciaCampusContext _context;
        public PacienteRepository(FarmaciaCampusContext context) : base(context)
        {
            _context=context;
        }
    }
}