using Application.Dtos;
using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfacesServicios
{
    public  interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto);
        Task<bool> RegisterAsync(RegisterRequestDTO dto);
    }
}
