using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class PacienteController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PacienteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    /// <summary>
    /// Retorna una lista de pacientes que han comprado por un producto (Consulta 12)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetPacientesCompraron/{producto}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PacienteDto>>> Get1(string producto)
    {
        var pacientes = await _unitOfWork.Pacientes.GetPacientesCompraron(producto);
        return _mapper.Map<List<PacienteDto>>(pacientes);
    }
    /// <summary>
    /// Retorna lista de pacientes que mas han gastado dinero en rango de fecha (consulta 22)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetPacientesMasGastaron/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PacientesMasGastaronDto>>> Get2(DateTime fechaInicio, DateTime fechaFinal)
    {
        var pacientes = await _unitOfWork.Pacientes.GetPacientesMasGastaron(fechaInicio, fechaFinal);
<<<<<<< HEAD
=======
        return _mapper.Map<List<PacientesMasGastaronDto>>(pacientes);
    }
    /// <summary>
    /// Retorna lista de pacientes que compraron un producto en rango de fecha (consulta 25)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetPacientesxProducto/{fechaInicio}&{fechaFinal}&{producto}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PacienteDto>>> Get3(DateTime fechaInicio, DateTime fechaFinal, string producto)
    {
        var pacientes = await _unitOfWork.Pacientes.GetPacientesxProducto(fechaInicio, fechaFinal, producto);
        return _mapper.Map<List<PacienteDto>>(pacientes);
    }
    /// <summary>
    /// Retorna lista de pacientes que no han comprado en rango de fecha (consulta 30)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetPacientesNoCompraron/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PacienteDto>>> Get4(DateTime fechaInicio, DateTime fechaFinal)
    {
        var pacientes = await _unitOfWork.Pacientes.GetPacientesNoCompraron(fechaInicio, fechaFinal);
        return _mapper.Map<List<PacienteDto>>(pacientes);
    }
      /// <summary>
    ///Retorna lista de pacientes con el total que han gastado (consulta 33)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetTotalGastadoPaciente/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PacientesMasGastaronDto>>> Get5(DateTime fechaInicio, DateTime fechaFinal)
    {
        var pacientes = await _unitOfWork.Pacientes.GetTotalGastadoPaciente(fechaInicio,fechaFinal);
>>>>>>> 9208e861474e91dfd173ae3e47577a24ef0734ac
        return _mapper.Map<List<PacientesMasGastaronDto>>(pacientes);
    }
    /// <summary>
    /// Retorna lista de pacientes que compraron un producto en rango de fecha (consulta 25)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetPacientesxProducto/{fechaInicio}&{fechaFinal}&{producto}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PacienteDto>>> Get3(DateTime fechaInicio, DateTime fechaFinal, string producto)
    {
        var pacientes = await _unitOfWork.Pacientes.GetPacientesxProducto(fechaInicio, fechaFinal, producto);
        return _mapper.Map<List<PacienteDto>>(pacientes);
    }
    /// <summary>
    /// Retorna lista de pacientes que no han comprado en rango de fecha (consulta 30)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetPacientesNoCompraron/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PacienteDto>>> Get4(DateTime fechaInicio, DateTime fechaFinal)
    {
        var pacientes = await _unitOfWork.Pacientes.GetPacientesNoCompraron(fechaInicio, fechaFinal);
        return _mapper.Map<List<PacienteDto>>(pacientes);
    }
}
