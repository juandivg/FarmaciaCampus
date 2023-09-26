using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    public class CompraController: BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CompraController(IUnitOfWork unitOfWork, IMapper mapper)
        {
                    this._unitOfWork = unitOfWork;
                    _mapper = mapper;
        }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
     public async  Task<ActionResult<IEnumerable<CompraxProductosDto>>> Get()
    {
        var compras = await _unitOfWork.Compras.GetAllAsync();
        return _mapper.Map<List<CompraxProductosDto>>(compras);
    }

    }
