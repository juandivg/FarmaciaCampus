using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    /// <summary>
    /// Retorna total de ventas por medicamento  (consulta 5)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetVentasxMedicament/{medicamento}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<VentasTotalesxProductoDto>> Get1(string medicamento)
    {
        var productos = await _unitOfWork.Ventas.GetVentasxMedicamento(medicamento);
        return _mapper.Map<VentasTotalesxProductoDto>(productos);
    }
    /// <summary>
    /// Retorna el total de dinero recaudado en las ventas  (consulta 8)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetTotalDineroVentas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<TotalDineroVentasDto>> Get2()
    {
        var total = await _unitOfWork.Ventas.GetTotalDineroVentas();
        return _mapper.Map<TotalDineroVentasDto>(total);
    }

    /// <summary>
    /// Retorna lista de cantidad de ventas por empleado en rango de fecha  (consulta 18)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetCantidadVentasxEmpleado/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<CantidadVentasxEmpleadoDto>>> Get3(DateTime fechaInicio, DateTime fechaFinal)
    {
        var cantidad = await _unitOfWork.Ventas.GetCantidadVentasxEmpleado(fechaInicio, fechaFinal);
        return _mapper.Map<List<CantidadVentasxEmpleadoDto>>(cantidad);
    }
    /// <summary>
    /// Retorna lista de empleados que hayan realizado mas ventas que la cantidad ingresada  (consulta 20)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetCantidadVentasxEmpleadoNumero/{cantidad}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<CantidadVentasxEmpleadoDto>>> Get4(int cantidad)
    {
        var empleado = await _unitOfWork.Ventas.GetCantidadVentasxEmpleadoNumero(cantidad);
        return _mapper.Map<List<CantidadVentasxEmpleadoDto>>(empleado);
    }
    /// <summary>
    /// Retorna lista con la cantidad de medicamentos por mes al año especificado (consulta 26)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetTotalMedicamentosAlMes/{anio}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<TotalMedicamentosAlMesDto>>> Get5(int anio)
    {
        var productos = await _unitOfWork.Ventas.GetTotalMedicamentosAlMes(anio);
        return _mapper.Map<List<TotalMedicamentosAlMesDto>>(productos);
    }
    /// <summary>
    /// Retorna lista de medicamentos por mes al año especificado (consulta 31)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetMedicamentosAlMes/{anio}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<MedicamentosAlMesDto>>> Get6(int anio)
    {
        var productos = await _unitOfWork.Ventas.GetMedicamentosAlMes(anio);
        return _mapper.Map<List<MedicamentosAlMesDto>>(productos);
    }
    /// <summary>
    ///Retorna lista de todas las ventas
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<VentaDto>>> Get7()
    {
        var ventas = await _unitOfWork.Ventas.GetAllAsync();
        return _mapper.Map<List<VentaDto>>(ventas);
    }
    /// <summary>
    ///Retorna el venta por ID 
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<VentaDto>> Get8(int id)
    {
        var venta = await _unitOfWork.Ventas.GetByIdAsync(id);
        return _mapper.Map<VentaDto>(venta);
    }
    /// <summary>
    ///Agregar venta
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Venta>> Post(VentaDto ventaDto)
    {
        var venta = _mapper.Map<Venta>(ventaDto);
        this._unitOfWork.Ventas.Add(venta);
        await _unitOfWork.SaveAsync();
        if (venta == null)
        {
            return BadRequest();
        }
        ventaDto.Id = venta.Id;
        return CreatedAtAction(nameof(Post), new { id = ventaDto.Id }, ventaDto);
    }
    /// <summary>
    /// Modificar la informacion de un venta, el id debe ser preciso
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Venta>> Put(int id, [FromBody] VentaDto ventaDto)
    {
        var venta = _mapper.Map<Venta>(ventaDto);
        if (venta == null)
        {
            return NotFound();
        }
        _unitOfWork.Ventas.Update(venta);
        await _unitOfWork.SaveAsync();
        return venta;
    }
    /// <summary>
    /// Eliminar una venta por ID
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        var venta = await _unitOfWork.Ventas.GetByIdAsync(id);
        if (venta == null)
        {
            return NotFound();
        }
        _unitOfWork.Ventas.Remove(venta);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
