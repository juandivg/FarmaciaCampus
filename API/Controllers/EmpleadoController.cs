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
        return _mapper.Map<List<EmpleadoDto>>(empleados);
    }
}
