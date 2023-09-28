using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class ProductoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet("GetProductosStock/{cantidad}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get1(int cantidad)
    {
        var productos = await _unitOfWork.Productos.GetProductosStock50(cantidad);
        return _mapper.Map<List<ProductoDto>>(productos);
    }
    [HttpGet("GetProveedoresxProductos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProveedoresxProductoDto>>> Get2()
    {
        var productos = await _unitOfWork.Productos.GetProveedoresxProductos();
        return _mapper.Map<List<ProveedoresxProductoDto>>(productos);
    }
    //     public async  Task<ActionResult<IEnumerable<ProductoDto>>> Get()
    // {
    //     var paises = await _unitOfWork.Productos.GetAllAsync();
    //     return _mapper.Map<List<ProductoDto>>(paises);
    // }
    [HttpGet("GetProductosxProveedor/{nombre}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get3(string nombre)
    {
        var productos = await _unitOfWork.Productos.GetProductosxProveedor(nombre);
        return _mapper.Map<List<ProductoDto>>(productos);
    }

    [HttpGet("GetProductosCaducadosAntes/{fechaVencimiento}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductosCaducadosxFechaDto>>> Get4(DateTime fechaVencimiento)
    {
        var productos = await _unitOfWork.Productos.GetProductosCaducadosAntes(fechaVencimiento);
        return _mapper.Map<List<ProductosCaducadosxFechaDto>>(productos);
    }
    [HttpGet("GetProductosSinVender")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get5()
    {
        var productos = await _unitOfWork.Productos.GetProductosSinVender();
        return _mapper.Map<List<ProductoDto>>(productos);
    }
    [HttpGet("GetProductosMasCaros")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get6()
    {
        var productos = await _unitOfWork.Productos.GetProductosMasCaros();
        return _mapper.Map<List<ProductoDto>>(productos);
    }
    [HttpGet("GetMedicamentosEnRango/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TotalVentasxRangoDto>> Get7(DateTime fechaInicio, DateTime fechaFinal)
    {
        var productos = await _unitOfWork.Productos.GetMedicamentosEnRango(fechaInicio, fechaFinal);
        return _mapper.Map<TotalVentasxRangoDto>(productos);
    }

    [HttpGet("GetMedicamentosMenosVendidos/{fechaInicio}&{fechaFinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get8(DateTime fechaInicio, DateTime fechaFinal)
    {
        var productos = await _unitOfWork.Productos.GetMedicamentosMenosVendidos(fechaInicio, fechaFinal);
        return _mapper.Map<List<ProductoDto>>(productos);
    }

    [HttpGet("GetPromedioProductosxVentas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PromedioProductosxVentaDto>>> Get9()
    {
        var promedio = await _unitOfWork.Productos.GetPromedioProductosxVentas();
        return _mapper.Map<List<PromedioProductosxVentaDto>>(promedio);
    }
    [HttpGet("GetProductosExpirados/{anio}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get10(int anio)
    {
        var productos = await _unitOfWork.Productos.GetProductosExpirados(anio);
        return _mapper.Map<List<ProductoDto>>(productos);
    }

}
