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
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<RecetaDto>>> Get1(DateTime fecha)
        {
            var recetas = await _unitOfWork.Recetas.GetRecetasFecha(fecha);
            return _mapper.Map<List<RecetaDto>>(recetas);
        }
        /// <summary>
        ///Retorna lista de todas las recetas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Doctor,Employee,Administrator")]
        public async Task<ActionResult<IEnumerable<RecetaFullDto>>> Get2()
        {
            var recetas = await _unitOfWork.Recetas.GetAllAsync();
            return _mapper.Map<List<RecetaFullDto>>(recetas);
        }
        /// <summary>
        ///Retorna el receta por ID 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Doctor,Employee,Administrator")]
        public async Task<ActionResult<RecetaFullDto>> Get3(int id)
        {
            var receta = await _unitOfWork.Recetas.GetByIdAsync(id);
            return _mapper.Map<RecetaFullDto>(receta);
        }
        /// <summary>
        ///Agregar receta
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Doctor,Administrator")]
        public async Task<ActionResult<Receta>> Post(RecetaFullDto recetaDto)
        {
            var receta = _mapper.Map<Receta>(recetaDto);
            this._unitOfWork.Recetas.Add(receta);
            await _unitOfWork.SaveAsync();
            if (receta == null)
            {
                return BadRequest();
            }
            recetaDto.Id = receta.Id;
            return CreatedAtAction(nameof(Post), new { id = recetaDto.Id }, recetaDto);
        }
        /// <summary>
        /// Modificar la informacion de un receta, el id debe ser preciso
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Receta>> Put(int id, [FromBody] RecetaFullDto recetaDto)
        {
            var receta = _mapper.Map<Receta>(recetaDto);
            if (receta == null)
            {
                return NotFound();
            }
            _unitOfWork.Recetas.Update(receta);
            await _unitOfWork.SaveAsync();
            return receta;
        }
        /// <summary>
        /// Eliminar una receta por ID
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int id)
        {
            var receta = await _unitOfWork.Recetas.GetByIdAsync(id);
            if (receta == null)
            {
                return NotFound();
            }
            _unitOfWork.Recetas.Remove(receta);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}