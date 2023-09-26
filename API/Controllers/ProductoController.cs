using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    public class ProductoController : BaseApiController
    {
         private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
                    this._unitOfWork = unitOfWork;
                    _mapper = mapper;
        }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
     public async  Task<ActionResult<IEnumerable<ProductoDto>>> Get1()
     {
        var productos= await _unitOfWork.Productos.GetProductosStock50();
        return _mapper.Map<List<ProductoDto>>(productos);
     }
    [HttpGet("Proveedoresporproducto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
     public async  Task<ActionResult<IEnumerable<ProveedoresxProductoDto>>> Get2()
     {
        var productos= await _unitOfWork.Productos.GetProveedoresxProductos();
        return _mapper.Map<List<ProveedoresxProductoDto>>(productos);
     }
    //     public async  Task<ActionResult<IEnumerable<ProductoDto>>> Get()
    // {
    //     var paises = await _unitOfWork.Productos.GetAllAsync();
    //     return _mapper.Map<List<ProductoDto>>(paises);
    // }

    }
