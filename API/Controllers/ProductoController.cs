using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using ApiIncidencias.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class ProductoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    /// <summary>
    /// Retorna lista de productos con menos de la cantidad ingresada (Consulta 1)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProductosStock/{cantidad}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get1(int cantidad)
    {
        var productos = await _unitOfWork.Productos.GetProductosStock50(cantidad);
        return _mapper.Map<List<ProductoDto>>(productos);
    }
    /// <summary>
    /// Retorna lista de proveedores con sus productos (consulta 2)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProveedoresxProductos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProveedoresxProductoDto>>> Get2()
    {
        var productos = await _unitOfWork.Productos.GetProveedoresxProductos();
        return _mapper.Map<List<ProveedoresxProductoDto>>(productos);
    }
    /// <summary>
    /// Retorna lista de medicamentos al proveedor que se le pase (consulta 3)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProductosxProveedor/{nombre}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get3(string nombre)
    {
        var productos = await _unitOfWork.Productos.GetProductosxProveedor(nombre);
        return _mapper.Map<List<ProductoDto>>(productos);
    }
    /// <summary>
    /// Retorna lista de medicamentos que caducan antes de la fecha (consulta 6)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProductosCaducadosAntes/{fechaVencimiento}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProductosCaducadosxFechaDto>>> Get4(DateTime fechaVencimiento)
    {
        var productos = await _unitOfWork.Productos.GetProductosCaducadosAntes(fechaVencimiento);
        return _mapper.Map<List<ProductosCaducadosxFechaDto>>(productos);
    }
    /// <summary>
    /// Retorna lista de productos sin vender (consulta 9,21)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProductosSinVender")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get5()
    {
        var productos = await _unitOfWork.Productos.GetProductosSinVender();
        return _mapper.Map<List<ProductoDto>>(productos);
    }
    /// <summary>
    /// Retorna lista de productos mas caros (consulta 10)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProductosMasCaros")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get6()
    {
        var productos = await _unitOfWork.Productos.GetProductosMasCaros();
        return _mapper.Map<List<ProductoDto>>(productos);
    }
    /// <summary>
    /// Retorna lista de productos vendidos en el rango de fecha (consulta 14,36)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetMedicamentosEnRango/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<TotalVentasxRangoDto>> Get7(DateTime fechaInicio, DateTime fechaFinal)
    {
        var productos = await _unitOfWork.Productos.GetMedicamentosEnRango(fechaInicio, fechaFinal);
        return _mapper.Map<TotalVentasxRangoDto>(productos);
    }
    /// <summary>
    /// Retorna lista de productos menos vendidos en el rango de fecha (consulta 15)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetMedicamentosMenosVendidos/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get8(DateTime fechaInicio, DateTime fechaFinal)
    {
        var productos = await _unitOfWork.Productos.GetMedicamentosMenosVendidos(fechaInicio, fechaFinal);
        return _mapper.Map<List<ProductoDto>>(productos);
    }

    /// <summary>
    /// Retorna lista de promedio de productos vendidos (consulta 17)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetPromedioProductosxVentas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<PromedioProductosxVentaDto>>> Get9()
    {
        var promedio = await _unitOfWork.Productos.GetPromedioProductosxVentas();
        return _mapper.Map<List<PromedioProductosxVentaDto>>(promedio);
    }
    /// <summary>
    /// Retorna lista de productos que han expirado en el rango de fecha (consulta 19)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProductosExpirados/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get10(DateTime fechaInicio, DateTime fechaFinal)
    {
        var productos = await _unitOfWork.Productos.GetProductosExpirados(fechaInicio, fechaFinal);
        return _mapper.Map<List<ProductoDto>>(productos);
    }
    /// <summary>
    /// Retorna lista de productos que no han sido vendidos en el rango de fecha (consulta 34)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProductosSinVenderFecha/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get11(DateTime fechaInicio, DateTime fechaFinal)
    {
        var productos = await _unitOfWork.Productos.GetProductosSinVenderFecha(fechaInicio, fechaFinal);
        return _mapper.Map<List<ProductoDto>>(productos);
    }
    /// <summary>
    /// Retorna lista de productos con precio mayor y stock menor a los parametros (consulta 38)
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetProductosPrecioStock/{precio}&{stock}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get12(int precio, int stock)
    {
        var productos = await _unitOfWork.Productos.GetProductosPrecioStock(precio, stock);
        return _mapper.Map<List<ProductoDto>>(productos);
    }
    /// <summary>
    ///Retorna lista de todos los Productos
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Employee,Doctor,Administrator")]
    public async Task<ActionResult<IEnumerable<ProductoFullDto>>> Get13()
    {
        var productos = await _unitOfWork.Productos.GetAllAsync();
        return _mapper.Map<List<ProductoFullDto>>(productos);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ProductoFullDto>>> Getpag([FromQuery] Params productoParams)
    {
        var pais = await _unitOfWork.Productos.GetAllAsync(productoParams.PageIndex,productoParams.PageSize,productoParams.Search);
        var lstPaisesDto = _mapper.Map<List<ProductoFullDto>>(pais.registros);
        return new Pager<ProductoFullDto>(lstPaisesDto,pais.totalRegistros,productoParams.PageIndex,productoParams.PageSize,productoParams.Search);
    }

    /// <summary>
    ///Retorna el producto por ID 
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Employee,Doctor,Administrator")]
    public async Task<ActionResult<ProductoFullDto>> Get14(int id)
    {
        var producto = await _unitOfWork.Productos.GetByIdAsync(id);
        return _mapper.Map<ProductoFullDto>(producto);
    }
    /// <summary>
    ///Agregar Producto
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Producto>> Post(ProductoFullDto productoDto)
    {
        var producto = _mapper.Map<Producto>(productoDto);
        this._unitOfWork.Productos.Add(producto);
        await _unitOfWork.SaveAsync();
        if (producto == null)
        {
            return BadRequest();
        }
        productoDto.Id = producto.Id;
        return CreatedAtAction(nameof(Post), new { id = productoDto.Id }, productoDto);
    }
    /// <summary>
    /// Modificar la informacion de un producto, el id debe ser preciso
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Producto>> Put(int id, [FromBody] ProductoFullDto productoDto)
    {
        var producto = _mapper.Map<Producto>(productoDto);
        if (producto == null)
        {
            return NotFound();
        }
        _unitOfWork.Productos.Update(producto);
        await _unitOfWork.SaveAsync();
        return producto;
    }
    /// <summary>
    /// Eliminar una producto por ID
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        var producto = await _unitOfWork.Productos.GetByIdAsync(id);
        if (producto == null)
        {
            return NotFound();
        }
        _unitOfWork.Productos.Remove(producto);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
