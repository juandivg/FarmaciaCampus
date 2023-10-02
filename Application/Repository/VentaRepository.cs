using System.Globalization;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
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

    public async Task<VentasTotalesxProducto> GetVentasxMedicamento(string medicamento)
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

    ).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TotalMedicamentosAlMes>> GetTotalMedicamentosAlMes(int anio)
    {
        return await (
            from v in _context.Ventas
            join pv in _context.ProductoVentas on v.Id equals pv.IdVentafk
            where v.Fecha.Year == anio
            group new { v, pv } by new { Mes = v.Fecha.Month, Anio = v.Fecha.Year } into g
            orderby g.Key.Anio, g.Key.Mes
            select new TotalMedicamentosAlMes
            {
                Mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Mes),
                TotalMedicamentos = g.Sum(x => x.pv.Cantidad)
            }
        ).ToListAsync();
    }
    public async Task<IEnumerable<MedicamentosAlMes>> GetMedicamentosAlMes(int anio)
    {
        return await (
    from v in _context.Ventas
    join pv in _context.ProductoVentas on v.Id equals pv.IdVentafk
    join p in _context.Productos on pv.IdProductofk equals p.Id
    where v.Fecha.Year == anio
    group new { v, pv, p } by new { Mes = v.Fecha.Month, Anio = v.Fecha.Year } into g
    orderby g.Key.Anio, g.Key.Mes
    select new MedicamentosAlMes
    {
        Mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Mes),
        Productos = g.Select(x => new Producto
        {
            Id = x.p.Id,
            NombreProducto = x.p.NombreProducto,
            Stock = x.p.Stock,
            PrecioC = x.p.PrecioC,
            PrecioV = x.p.PrecioV,
            IdTipoProductofk = x.p.IdTipoProductofk
        }).ToList()
    }
).ToListAsync();
    }
    public override async Task<IEnumerable<Venta>> GetAllAsync()
    {
        return await _context.Ventas
                        .Include(p => p.Empleado)
                        .Include(p => p.Paciente)
                        .Include(p => p.Receta)
                        .ToListAsync();
    }
    public override async Task<Venta> GetByIdAsync(int id)
    {
        return await _context.Ventas
                        .Include(p => p.Empleado)
                        .Include(p => p.Paciente)
                        .Include(p => p.Receta)
                        .FirstOrDefaultAsync(p => p.Id == id);
    }

}

