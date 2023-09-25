using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Producto,ProductoDto>().ReverseMap().ForMember(o=>o.PrecioC,d=>d.Ignore());
            CreateMap<Producto,ProductoDto>().ReverseMap().ForMember(o=>o.ProductoCompras,d=>d.Ignore());
            CreateMap<Producto,ProductoDto>().ReverseMap().ForMember(o=>o.ProductoRecetas,d=>d.Ignore());
            CreateMap<Producto,ProductoDto>().ReverseMap().ForMember(o=>o.ProductoVentas,d=>d.Ignore());
            CreateMap<Producto,ProductoDto>().ForMember(o=>o.Proveedores,o=>o.MapFrom(o=>o.ProveedorProductos.Select(o=>o.Proveedor)));
            CreateMap<Proveedor,ProveedorDto>().ReverseMap();
            
        }
    }
}