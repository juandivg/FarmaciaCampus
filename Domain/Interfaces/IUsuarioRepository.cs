using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;
    public interface IUsuarioRepository:IGenericRepository<Usuario>
    {
        Task<Usuario> GetByUsername(string username);
        Task<Usuario> GetByRefreshToken(string username);
    }
