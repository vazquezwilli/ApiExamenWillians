using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<EmpleadoDTO>> GetAllAsync();
        Task<bool> UpdateAsync(EmpleadoDTO dto);
        Task<int> CreateAsync(EmpleadoDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
