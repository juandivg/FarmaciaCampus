using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRecetaRepository : IGenericRepository<Receta>
    {
        Task<IEnumerable<Receta>> GetRecetasFecha(DateTime fecha);
    }
}