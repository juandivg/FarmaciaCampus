using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RecetaController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RecetaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// Retorna lista de recetas emitidas despues de la fecha (consulta 4)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRecetasFecha/{fecha}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RecetaDto>>> Get1(DateTime fecha)
        {
            var recetas = await _unitOfWork.Recetas.GetRecetasFecha(fecha);
            return _mapper.Map<List<RecetaDto>>(recetas);
        }
    }
}