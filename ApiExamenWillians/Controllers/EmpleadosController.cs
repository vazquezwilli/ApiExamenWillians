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
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;
        private readonly IValidator<EmpleadoDTO> _validator;
        public EmpleadosController(IEmpleadoService empleadoService, IValidator<EmpleadoDTO> validator)
        {
            _empleadoService = empleadoService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoDTO>>> GetAll()
        {
            var empleados = await _empleadoService.GetAllAsync();
            return Ok(empleados);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] EmpleadoDTO empleadoDto)
        {
            #region validaciones
            var validationResult = await _validator.ValidateAsync(empleadoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new {
                    Campo = e.PropertyName,
                    Mensaje = e.ErrorMessage
                }));
            }
            #endregion


            var newId = await _empleadoService.CreateAsync(empleadoDto);

            return CreatedAtAction(null,new { id = newId });
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] EmpleadoDTO empleadoDto)
        {
            #region validaciones
            var validationResult = await _validator.ValidateAsync(empleadoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new {
                    Campo = e.PropertyName,
                    Mensaje = e.ErrorMessage
                }));
            }
            #endregion

            var updated = await _empleadoService.UpdateAsync(empleadoDto);

            if (!updated) return NotFound($"No se encontró el empleado con ID {id}");

            return Ok("Actualizado con exito"); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _empleadoService.DeleteAsync(id);

            if (!deleted) return NotFound($"No se encontró el empleado con ID {id}");

            return Ok("Eliminado con exito");

        }
    }
}
