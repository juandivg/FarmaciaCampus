using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleadoRepository
    {
        private readonly FarmaciaCampusContext _context;
        public EmpleadoRepository(FarmaciaCampusContext context) : base(context)
        {
            _context=context;
        }
    }
}