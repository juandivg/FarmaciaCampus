using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Views;

namespace API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Producto, ProductoDto>().ReverseMap().ForMember(o => o.PrecioC, d => d.Ignore())
            .ForMember(o => o.ProductoCompras, d => d.Ignore())
            .ForMember(o => o.ProductoRecetas, d => d.Ignore())
            .ForMember(o => o.ProductoVentas, d => d.Ignore());
            //CreateMap<ProveedoresxProducto, ProveedoresxProductoDto>().ReverseMap();
            CreateMap<ProveedoresxProducto, ProveedoresxProductoDto>()
            .ForMember(dest => dest.Proveedores, opt => opt.MapFrom(src => src.Proveedores.Select(p => new ProveedorDto
            {
                Id = p.Id,
                NombreProveedor = p.NombreProveedor,
                Correo = p.Correo
            }))).ReverseMap();
            CreateMap<Proveedor, ProveedorDto>().ReverseMap();

            CreateMap<Receta, RecetaDto>().ReverseMap();
            CreateMap<VentasTotalesxProducto, VentasTotalesxProductoDto>().ReverseMap();
            CreateMap<CantidadVentasxProveedor, CantidadVentasxProveedorDto>().ReverseMap();
            CreateMap<TotalDineroVentas, TotalDineroVentasDto>().ReverseMap();

            // CreateMap<Producto,ProductoDto>().ForMember(o=>o.Proveedores,o=>o.MapFrom(o=>o.ProveedorProductos.Select(o=>o.Proveedor)));
            // CreateMap<Proveedor,ProveedorDto>().ReverseMap();
            // //CreateMap<Compra,CompraxProductosDto>().ForMember(p=>p.Proveedores,p=>p.MapFrom(p=>p.productoCompras.Select(p=>p.Producto.ProveedorProductos.Select(p=>p.Proveedor)))).ReverseMap();
            // //hola
            //   //CreateMap<Compra,ProductoDto>().ReverseMap().ForMember(o=>o.ProductoVentas,d=>d.Ignore());
            // CreateMap<Compra,CompraxProductosDto>().ReverseMap();
            //CreateMap<Compra,CompraxProductosDto>()
            // //.ForMember(d=>d.Proveedores,o=>o.MapFrom(src=>src.productoCompras.Select(p=>p.Producto.ProveedorProductos.Select(p=>p.Proveedor))));
            // .ForMember(d=>d.Productos,o=>o.MapFrom(src=>src.productoCompras.Select(p=>p.Producto)));

            CreateMap<ProductosCaducadosxFecha, ProductosCaducadosxFechaDto>().ReverseMap();
            CreateMap<TotalProductosxProveedor, TotalProductosxProveedorDto>().ReverseMap();
            CreateMap<Paciente, PacienteDto>().ReverseMap();
            CreateMap<TotalVentasxRango, TotalVentasxRangoDto>().ReverseMap();
            CreateMap<GananciaTotalxProveedor, GananciaTotalxProveedorDto>().ReverseMap();
            CreateMap<PromedioProductosxVenta, PromedioProductosxVentaDto>().ReverseMap();
            CreateMap<CantidadVentasxEmpleado, CantidadVentasxEmpleadoDto>().ReverseMap();
        }
    }
}