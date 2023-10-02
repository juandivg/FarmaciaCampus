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
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class EmpleadoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EmpleadoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    /// <summary>
    /// Retorna lista de empleado sin ventas en el rango de fecha especificado (Consulta 23,37)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetEmpleadosSinVentas/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get1(DateTime fechaInicio, DateTime fechaFinal)
    {
        var empleados = await _unitOfWork.Empleados.GetEmpleadosSinVentas(fechaInicio, fechaFinal);
        return _mapper.Map<List<EmpleadoDto>>(empleados);
    }
    /// <summary>
    /// Retorna lista de empleado con menos ventas que la cantidad por rango de fecha (Consulta 27)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetEmpleadosConMenosVentas/{fechaInicio}&{fechaFinal}&{cantidad}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<EmpleadosxMenosCantidadVentasDto>>> Get2(DateTime fechaInicio, DateTime fechaFinal, int cantidad)
    {
        var empleados = await _unitOfWork.Empleados.GetEmpleadosConMenosVentas(fechaInicio, fechaFinal, cantidad);
        return _mapper.Map<List<EmpleadosxMenosCantidadVentasDto>>(empleados);
    }
    /// <summary>
    /// Retorna el empleado con mas productos diferentes vendidos en un año (Consulta 32)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetEmpleadoConMasProductos/{anio}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get3(int anio)
    {
        var empleados = await _unitOfWork.Empleados.GetEmpleadoConMasProductos(anio);
        return _mapper.Map<List<EmpleadoDto>>(empleados);
    }
    /// <summary>
    ///Retorna lista de todos los empleados
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get4()
    {
        var empleados = await _unitOfWork.Empleados.GetAllAsync();
        return _mapper.Map<List<EmpleadoDto>>(empleados);
    }
    /// <summary>
    ///Retorna el empleado por ID 
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<EmpleadoDto>> Get5(int id)
    {
        var empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
        return _mapper.Map<EmpleadoDto>(empleado);
    }
    /// <summary>
    ///Agregar Proveedor
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Empleado>> Post(EmpleadoDto empleadoDto)
    {
        var empleado = _mapper.Map<Empleado>(empleadoDto);
        this._unitOfWork.Empleados.Add(empleado);
        await _unitOfWork.SaveAsync();
        if (empleado == null)
        {
            return BadRequest();
        }
        empleadoDto.Id = empleado.Id;
        return CreatedAtAction(nameof(Post), new { id = empleadoDto.Id }, empleadoDto);
    }
    /// <summary>
    /// Modificar la informacion de un proveedor, el id debe ser preciso
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Empleado>> Put(int id, [FromBody] EmpleadoDto empleadoDto)
    {
        var empleado = _mapper.Map<Empleado>(empleadoDto);
        if (empleado == null)
        {
            return NotFound();
        }
        _unitOfWork.Empleados.Update(empleado);
        await _unitOfWork.SaveAsync();
        return empleado;
    }
    /// <summary>
    /// Eliminar una paciente por ID
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        var empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
        if (empleado == null)
        {
            return NotFound();
        }
        _unitOfWork.Empleados.Remove(empleado);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
