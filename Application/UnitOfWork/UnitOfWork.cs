using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FarmaciaCampusContext _context;
        private IEmpleadoRepository _empleados;
        private IProveedorRepository _proveedores;
        private IPacienteRepository _pacientes;
        private IProductoRepository _productos;
        private IUsuarioRepository _usuarios;
        private IRolRepository _roles;
        private ICompraRepository _compras;
        private IRecetaRepository _recetas;
        private IVentaRepository _ventas;
        public UnitOfWork(FarmaciaCampusContext context)
        {
            _context= context;
        }
        public IEmpleadoRepository Empleados
        {
            get{
                if(_empleados==null)
                {
                    _empleados=new EmpleadoRepository(_context);
                }
                return _empleados;
            }
        }
        public ICompraRepository Compras
        {
            get{
                if(_compras==null)
                {
                    _compras=new CompraRepository(_context);
                }
                return _compras;
            }
        }
        public IProveedorRepository Proveedores
        {
            get{
                if(_proveedores==null)
                {
                    _proveedores=new ProveedorRepository(_context);
                }
                return _proveedores;
            }
        }
        public IPacienteRepository Pacientes
        {
            get{
                if(_pacientes==null)
                {
                    _pacientes=new PacienteRepository(_context);
                }
                return _pacientes;
            }
        }
        public IProductoRepository Productos
        {
            get{
                if(_productos==null)
                {
                    _productos=new ProductoRepository(_context);
                }
                return _productos;
            }
        }
        public IUsuarioRepository Usuarios
        {
            get{
                if(_usuarios==null)
                {
                    _usuarios=new UsuarioRepository(_context);
                }
                return _usuarios;
            }
        }
        public IRolRepository Roles
        {
            get{
                if(_roles==null)
                {
                    _roles=new RolRepositorio(_context);
                }
                return _roles;
            }
        }
        public IRecetaRepository Recetas
        {
            get{
                if(_recetas==null)
                {
                    _recetas=new RecetaRepository(_context);
                }
                return _recetas;
            }
        }
        public IVentaRepository Ventas
        {
            get{
                if(_ventas==null)
                {
                    _ventas=new VentaRepository(_context);
                }
                return _ventas;
            }
        }
    
        
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}