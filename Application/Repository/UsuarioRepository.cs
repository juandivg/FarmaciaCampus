using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly FarmaciaCampusContext _context;
        public UsuarioRepository(FarmaciaCampusContext context) : base(context)
        {
            _context=context;
        }
    }
}