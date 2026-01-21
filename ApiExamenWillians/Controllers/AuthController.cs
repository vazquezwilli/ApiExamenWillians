using Application.Dtos;
using Application.InterfacesServicios;
using Application.Servicios;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiExamenWillians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (!result.Success)
            {
                return Unauthorized(new { message = result.Message });
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (!result)
            {
                return BadRequest(new { message = "El nombre de usuario ya existe o no se pudo registrar." });
            }

            return Ok(new { message = "Usuario registrado exitosamente" });
        }
    }
}
