using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    public class VentaController:BaseApiController
    {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public VentaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet("GetVentasxMedicament/{medicamento}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<VentasTotalesxProductoDto>>> Get1(string medicamento)
    {
        var productos = await _unitOfWork.Ventas.GetVentasxMedicamento(medicamento);
        return _mapper.Map<List<VentasTotalesxProductoDto>>(productos);
    }
    }
