using Application.Dtos;
using Application.InterfacesServicios;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiExamenWillians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AreasController : ControllerBase
    {
        private readonly IAreaService _serviceArea;
        private readonly IValidator<AreaDTO> _validator;

        public AreasController(IAreaService serviceArea, IValidator<AreaDTO> validator)
        {
            _serviceArea = serviceArea;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaDTO>>> GetAll()
        {
            var areas = await _serviceArea.GetAllAsync();
            return Ok(areas);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] AreaDTO dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new {
                    Campo = e.PropertyName,
                    Mensaje = e.ErrorMessage
                }));
            }

            var newId = await _serviceArea.CreateAsync(dto);

            return CreatedAtAction(null,new { id = newId });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AreaDTO dto)
        {

            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new {
                    Campo = e.PropertyName,
                    Mensaje = e.ErrorMessage
                }));
            }

            var updated = await _serviceArea.UpdateAsync(dto);

            if (!updated) return NotFound($"No se encontró el area con ID {dto.AreaId}");

            return Ok("Actualizado con exito");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _serviceArea.DeleteAsync(id);

            if (!deleted) return NotFound($"No se encontró el area con ID {id}");

            return Ok("Eliminado con exito");
        }
    }
}