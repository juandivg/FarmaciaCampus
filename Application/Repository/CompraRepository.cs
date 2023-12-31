using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository
{
    public class CompraRepository : GenericRepository<Compra>, ICompraRepository
    {
        private readonly FarmaciaCampusContext _context;
        public CompraRepository(FarmaciaCampusContext context) : base(context)
        {
            _context = context;
        }

    }
}