using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class ProveedorController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProveedorController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet("GetProveedoresSinCompras")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProveedorDto>>> Get1()
    {
        var proveedores = await _unitOfWork.Proveedores.GetProveedoresSinCompras();
        return _mapper.Map<List<ProveedorDto>>(proveedores);
    }
    [HttpGet("GetCatidadVentasxProveedors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CantidadVentasxProveedorDto>>> Get2()
    {
        var proveedores = await _unitOfWork.Proveedores.GetCantidadVentasxProveedors();
        return _mapper.Map<List<CantidadVentasxProveedorDto>>(proveedores);
    }
}

