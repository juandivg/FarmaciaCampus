using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence;

namespace Application.Repository;
public class VentaRepository : GenericRepository<Venta>, IVentaRepository
{
    private readonly FarmaciaCampusContext _context;
    public VentaRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CantidadVentasxEmpleado>> GetCantidadVentasxEmpleado(DateTime fechaInicio, DateTime fechaFinal)
    {
              return await (
            from v in _context.Ventas
            join emp in _context.Empleados on v.IdEmpleadofk equals emp.Id
            where v.Fecha >= fechaInicio && v.Fecha <= fechaFinal
            group v by emp into grupo
            select new CantidadVentasxEmpleado
            {
                Id = grupo.Key.Id,
                NombreEmpleado = grupo.Key.NombreEmpleado,
                CantidadVentas = grupo.Count()
            }
        ).ToListAsync();
    }

    public async Task<IEnumerable<CantidadVentasxEmpleado>> GetCantidadVentasxEmpleadoNumero(int cantidad)
    {
            return await (
            from v in _context.Ventas
            join emp in _context.Empleados on v.IdEmpleadofk equals emp.Id
            group v by new { emp.Id, emp.NombreEmpleado } into grupo
            where grupo.Count() > cantidad
            select new CantidadVentasxEmpleado
            {
                Id = grupo.Key.Id,
                NombreEmpleado = grupo.Key.NombreEmpleado,
                CantidadVentas = grupo.Count()
            }
        ).ToListAsync();
    }

    public async Task<TotalDineroVentas> GetTotalDineroVentas()
    {
        decimal TotalV = await (
            from pro in _context.Productos
            join pv in _context.ProductoVentas on pro.Id equals pv.IdProductofk
            select pro.PrecioV).SumAsync();
        return new TotalDineroVentas
        {
            Total = TotalV
        };


    }

    public async Task<IEnumerable<VentasTotalesxProducto>> GetVentasxMedicamento(string medicamento)
    {
            return await (
         from pro in _context.Productos
         join pp in _context.ProductoVentas on pro.Id equals pp.IdProductofk
         join ven in _context.Ventas on pp.IdVentafk equals ven.Id
         where pro.NombreProducto == medicamento
         group pp by new { pro.NombreProducto } into grp
         select new VentasTotalesxProducto
         {
             NombreProducto = grp.Key.NombreProducto,
             Cantidad = grp.Sum(pp => pp.Cantidad)
         }

        ).ToListAsync();
    }


  
}

