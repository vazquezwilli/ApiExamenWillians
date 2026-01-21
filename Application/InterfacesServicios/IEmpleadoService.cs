using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfacesServicios
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<EmpleadoDTO>> GetAllAsync();
        Task<int> CreateAsync(EmpleadoDTO dto);
        Task<bool> UpdateAsync(EmpleadoDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
