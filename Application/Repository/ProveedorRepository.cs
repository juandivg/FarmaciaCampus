using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class ProveedorRepository : GenericRepository<Proveedor>, IProveedorRepository

    {
        private readonly FarmaciaCampusContext _context;
        public ProveedorRepository(FarmaciaCampusContext context) : base(context)
        {
            _context=context;
        }
    }
}