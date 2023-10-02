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
    [Authorize(Roles = "Administrator")]
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
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<PacientesMasGastaronDto>>> Get2(DateTime fechaInicio, DateTime fechaFinal)
    {
        var pacientes = await _unitOfWork.Pacientes.GetPacientesMasGastaron(fechaInicio, fechaFinal);
        return _mapper.Map<List<PacientesMasGastaronDto>>(pacientes);
    }
    /// <summary>
    /// Retorna lista de pacientes que compraron un producto en rango de fecha (consulta 25)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetPacientesxProducto/{fechaInicio}&{fechaFinal}&{producto}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
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
    [Authorize(Roles = "Administrator")]
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
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<PacientesMasGastaronDto>>> Get5(DateTime fechaInicio, DateTime fechaFinal)
    {
        var pacientes = await _unitOfWork.Pacientes.GetTotalGastadoPaciente(fechaInicio, fechaFinal);
        return _mapper.Map<List<PacientesMasGastaronDto>>(pacientes);
    }
    /// <summary>
    ///Retorna lista de todos los pacientes 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Employee,Administrator")]
    public async Task<ActionResult<IEnumerable<PacienteDto>>> Get6()
    {
        var pacientes = await _unitOfWork.Pacientes.GetAllAsync();
        return _mapper.Map<List<PacienteDto>>(pacientes);
    }
    /// <summary>
    ///Retorna el paciente por ID 
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Employee,Administrator")]
    public async Task<ActionResult<PacienteDto>> Get7(int id)
    {
        var personas = await _unitOfWork.Pacientes.GetByIdAsync(id);
        return _mapper.Map<PacienteDto>(personas);
    }
    /// <summary>
    ///Agregar Paciente
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Employee,Administrator")]
    public async Task<ActionResult<Paciente>> Post(PacienteDto pacienteDto)
    {
        var paciente = _mapper.Map<Paciente>(pacienteDto);
        this._unitOfWork.Pacientes.Add(paciente);
        await _unitOfWork.SaveAsync();
        if (paciente == null)
        {
            return BadRequest();
        }
        pacienteDto.Id = paciente.Id;
        return CreatedAtAction(nameof(Post), new { id = pacienteDto.Id }, pacienteDto);
    }
    /// <summary>
    /// Modificar la informacion de un paciente, el id debe ser preciso
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Employee,Administrator")]
    public async Task<ActionResult<Paciente>> Put(int id, [FromBody] PacienteDto pacienteDto)
    {
        var paciente = _mapper.Map<Paciente>(pacienteDto);
        if (paciente == null)
        {
            return NotFound();
        }
        _unitOfWork.Pacientes.Update(paciente);
        await _unitOfWork.SaveAsync();
        return paciente;
    }
    /// <summary>
    /// Eliminar una paciente por ID
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Employee,Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        var paciente = await _unitOfWork.Pacientes.GetByIdAsync(id);
        if (paciente == null)
        {
            return NotFound();
        }
        _unitOfWork.Pacientes.Remove(paciente);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
