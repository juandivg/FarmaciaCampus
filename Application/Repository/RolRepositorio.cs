using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class RolRepositorio : GenericRepository<Rol>, IRolRepository
    {
        private readonly FarmaciaCampusContext _context;
        public RolRepositorio(FarmaciaCampusContext context) : base(context)
        {
            _context=context;
        }
    }
}