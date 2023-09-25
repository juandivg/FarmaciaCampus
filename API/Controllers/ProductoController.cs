using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
        public async  Task<ActionResult<IEnumerable<ProductoDto>>> Get()
    {
        var paises = await _unitOfWork.Productos.GetAllAsync();
        return _mapper.Map<List<ProductoDto>>(paises);
    }

    }
}