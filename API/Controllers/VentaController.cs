using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class VentaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public VentaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet("GetVentasxMedicament/{medicamento}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<VentasTotalesxProductoDto>>> Get1(string medicamento)
    {
        var productos = await _unitOfWork.Ventas.GetVentasxMedicamento(medicamento);
        return _mapper.Map<List<VentasTotalesxProductoDto>>(productos);
    }
    [HttpGet("GetTotalDineroVentas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TotalDineroVentasDto>> Get2()
    {
        var total = await _unitOfWork.Ventas.GetTotalDineroVentas();
        return _mapper.Map<TotalDineroVentasDto>(total);
    }

    [HttpGet("GetCantidadVentasxEmpleado/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CantidadVentasxEmpleadoDto>>> Get3(DateTime fechaInicio, DateTime fechaFinal)
    {
        var cantidad = await _unitOfWork.Ventas.GetCantidadVentasxEmpleado(fechaInicio, fechaFinal);
        return _mapper.Map<List<CantidadVentasxEmpleadoDto>>(cantidad);
    }
    [HttpGet("GetCantidadVentasxEmpleadoNumero/{cantidad}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CantidadVentasxEmpleadoDto>>> Get4(int cantidad)
    {
        var empleado = await _unitOfWork.Ventas.GetCantidadVentasxEmpleadoNumero(cantidad);
        return _mapper.Map<List<CantidadVentasxEmpleadoDto>>(empleado);
    }
}
