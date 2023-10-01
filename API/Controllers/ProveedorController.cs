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
    /// <summary>
    /// Retorna lista con la cantidad de ventas por proveedor (consulta 7)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetCatidadVentasxProveedors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    public async Task<ActionResult<IEnumerable<ProveedoresConMasProductosDto>>> Get9(int cantidad)
    {
        var proveedores = await _unitOfWork.Proveedores.GetProveedoresxProductos(cantidad);
        return _mapper.Map<List<ProveedoresConMasProductosDto>>(proveedores);
    }
}