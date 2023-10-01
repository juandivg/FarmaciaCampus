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
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly FarmaciaCampusContext _context;
        public UsuarioRepository(FarmaciaCampusContext context) : base(context)
        {
            _context=context;
        }
            public async Task<Usuario> GetByRefreshToken(string refreshToken)
    {
        return await _context.Usuarios
                .Include(u=>u.Roles)
                .Include(u=>u.RefreshTokens)
                .FirstOrDefaultAsync(u=>u.RefreshTokens.Any(t=>t.Token==refreshToken));
    }

    public async Task<Usuario> GetByUsername(string username)
    {
        return await _context.Usuarios
                    .Include(u=>u.Roles)
                    .Include(u=>u.RefreshTokens)
                    .FirstOrDefaultAsync(u=>u.Username.ToLower()==username.ToLower());
    }
    }
}