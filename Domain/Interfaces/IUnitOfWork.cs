using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces;
    public interface IUnitOfWork
    {
        IEmpleadoRepository Empleados {get;}
        IPacienteRepository Pacientes {get;}
        IProveedorRepository Proveedores {get;}
        IProductoRepository Productos {get;}
        IUsuarioRepository Usuarios    {get;}
        IRolRepository Roles {get;}
        ICompraRepository Compras {get;}
        IRecetaRepository Recetas {get;}
        IVentaRepository Ventas {get;}
        Task<int> SaveAsync();

     }
