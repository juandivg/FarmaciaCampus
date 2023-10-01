using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
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
    /// Retorna lista de empleado sin ventas en el rango de fecha especificado (Consulta 23)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetEmpleadosSinVentas/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get1(DateTime fechaInicio, DateTime fechaFinal)
    {
        var empleados = await _unitOfWork.Empleados.GetEmpleadosSinVentas(fechaInicio, fechaFinal);
<<<<<<< HEAD
=======
        return _mapper.Map<List<EmpleadoDto>>(empleados);
    }
    /// <summary>
    /// Retorna lista de empleado con menos ventas que la cantidad por rango de fecha (Consulta 27)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetEmpleadosConMenosVentas/{fechaInicio}&{fechaFinal}&{cantidad}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmpleadosxMenosCantidadVentasDto>>> Get2(DateTime fechaInicio, DateTime fechaFinal, int cantidad)
    {
        var empleados = await _unitOfWork.Empleados.GetEmpleadosConMenosVentas(fechaInicio, fechaFinal, cantidad);
        return _mapper.Map<List<EmpleadosxMenosCantidadVentasDto>>(empleados);
    }
     /// <summary>
    /// Retorna el empleado con mas productos diferentes vendidos en un año (Consulta 31)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetEmpleadoConMasProductos/{anio}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get3( int anio)
    {
        var empleados = await _unitOfWork.Empleados.GetEmpleadoConMasProductos(anio);
>>>>>>> 9208e861474e91dfd173ae3e47577a24ef0734ac
        return _mapper.Map<List<EmpleadoDto>>(empleados);
    }
    /// <summary>
    /// Retorna lista de empleado con menos ventas que la cantidad por rango de fecha (Consulta 27)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetEmpleadosConMenosVentas/{fechaInicio}&{fechaFinal}&{cantidad}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmpleadosxMenosCantidadVentasDto>>> Get2(DateTime fechaInicio, DateTime fechaFinal, int cantidad)
    {
        var empleados = await _unitOfWork.Empleados.GetEmpleadosConMenosVentas(fechaInicio, fechaFinal, cantidad);
        return _mapper.Map<List<EmpleadosxMenosCantidadVentasDto>>(empleados);
    }
}
