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
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProveedorDto>>> Get1()
    {
        var proveedores = await _unitOfWork.Proveedores.GetProveedoresSinCompras();
        return _mapper.Map<List<ProveedorDto>>(proveedores);
    }
    /// <summary>
    /// Retorna lista con la cantidad de ventas por proveedor (consulta 7)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetCatidadVentasxProveedors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<CantidadVentasxProveedorDto>>> Get2()
    {
        var proveedores = await _unitOfWork.Proveedores.GetCantidadVentasxProveedors();
        return _mapper.Map<List<CantidadVentasxProveedorDto>>(proveedores);
    }
    /// <summary>
    /// Retorna lista del total de productos por proveedor (consulta 11)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetTotalProductosxProveedor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<TotalProductosxProveedorDto>>> Get3()
    {
        var proveedores = await _unitOfWork.Proveedores.GetTotalProductosxProveedor();
        return _mapper.Map<List<TotalProductosxProveedorDto>>(proveedores);
    }
    /// <summary>
    /// Retorna lista de proveedores que no han vendido en el rango de fecha (consulta 13)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProveedoresSinVentas/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProveedorDto>>> Get4(DateTime fechaInicio, DateTime fechaFinal)
    {
        var proveedores = await _unitOfWork.Proveedores.GetProveedoresSinVentas(fechaInicio, fechaFinal);
        return _mapper.Map<List<ProveedorDto>>(proveedores);
    }
    /// <summary>
    /// Retorna lista de la ganancia total por proveedor en el rango de fecha (consulta 16)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetGananciaTotalxProveedor/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<GananciaTotalxProveedorDto>>> Get5(DateTime fechaInicio, DateTime fechaFinal)
    {
        var proveedores = await _unitOfWork.Proveedores.GetGananciaTotalxProveedor(fechaInicio, fechaFinal);
        return _mapper.Map<List<GananciaTotalxProveedorDto>>(proveedores);
    }
    /// <summary>
    /// Retorna lista de proveedores con mas productos suministrados por rango de fecha (consulta 24)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProveedoresConMasProductos/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProveedoresConMasProductosDto>>> Get6(DateTime fechaInicio, DateTime fechaFinal)
    {
        var proveedores = await _unitOfWork.Proveedores.GetProveedoresConMasProductos(fechaInicio, fechaFinal);
        return _mapper.Map<List<ProveedoresConMasProductosDto>>(proveedores);
    }
    /// <summary>
    /// Retorna el total de proveedores por el rango de fecha (consulta 28)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetTotalProveedoresSuministraron/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<TotalProveedoresSuministraronDto>> Get7(DateTime fechaInicio, DateTime fechaFinal)
    {
        var cantidad = await _unitOfWork.Proveedores.GetTotalProveedoresSuministraron(fechaInicio, fechaFinal);
        return _mapper.Map<TotalProveedoresSuministraronDto>(cantidad);
    }
    /// <summary>
    /// Retorna lista proveedores con menos de la cantidad ingresada de medicamentos en stock (consulta 29)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProveedoresxMenosMedicamentos/{cantidad}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProveedorDto>>> Get8(int cantidad)
    {
        var proveedores = await _unitOfWork.Proveedores.GetProveedoresxMenosMedicamentos(cantidad);
        return _mapper.Map<List<ProveedorDto>>(proveedores);
    }
    /// <summary>
    /// Retorna lista proveedores con al manos la cantidad de productos ingresada (consulta 35)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProveedoresxProductos/{cantidad}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProveedoresConMasProductosDto>>> Get9(int cantidad)
    {
        var proveedores = await _unitOfWork.Proveedores.GetProveedoresxProductos(cantidad);
        return _mapper.Map<List<ProveedoresConMasProductosDto>>(proveedores);
    }
    /// <summary>
    ///Retorna lista de todos los proveedores
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProveedorFullDto>>> Get10()
    {
        var proveedores = await _unitOfWork.Proveedores.GetAllAsync();
        return _mapper.Map<List<ProveedorFullDto>>(proveedores);
    }
    /// <summary>
    ///Retorna el proveedor por ID 
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ProveedorFullDto>> Get11(int id)
    {
        var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id);
        return _mapper.Map<ProveedorFullDto>(proveedor);
    }
    /// <summary>
    ///Agregar Proveedor
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Proveedor>> Post(ProveedorFullDto proveedorDto)
    {
        var proveedor = _mapper.Map<Proveedor>(proveedorDto);
        this._unitOfWork.Proveedores.Add(proveedor);
        await _unitOfWork.SaveAsync();
        if (proveedor == null)
        {
            return BadRequest();
        }
        proveedorDto.Id = proveedor.Id;
        return CreatedAtAction(nameof(Post), new { id = proveedorDto.Id }, proveedorDto);
    }
    /// <summary>
    /// Modificar la informacion de un proveedor, el id debe ser preciso
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Proveedor>> Put(int id, [FromBody] ProveedorFullDto proveedorDto)
    {
        var proveedor = _mapper.Map<Proveedor>(proveedorDto);
        if (proveedor == null)
        {
            return NotFound();
        }
        _unitOfWork.Proveedores.Update(proveedor);
        await _unitOfWork.SaveAsync();
        return proveedor;
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
        var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id);
        if (proveedor == null)
        {
            return NotFound();
        }
        _unitOfWork.Proveedores.Remove(proveedor);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}