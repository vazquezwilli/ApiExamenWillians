using Application.Dtos;
using Application.InterfacesServicios;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Servicios
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto)
        {
            var usuario = await _context.Usuarios
                  .Include(u => u.Rol) 
                  .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
            {
                return new LoginResponseDTO { Success = false, Message = "Usuario o contraseña incorrectos" };
            }

            var token = GenerateJwt(usuario);

            return new LoginResponseDTO { Success = true, Token = token, Message = "Bienvenido" };
        }

        private string GenerateJwt(Usuario usuario)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim("UsuarioId", usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Role, usuario.Rol?.Nombre.ToString() ?? "User")
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<bool> RegisterAsync(RegisterRequestDTO dto)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.Username == dto.Username);
            if (existe) return false;

            var nuevoUsuario = new Usuario
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RolId = dto.RolId,
                Activo= true
            };

            _context.Usuarios.Add(nuevoUsuario);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
