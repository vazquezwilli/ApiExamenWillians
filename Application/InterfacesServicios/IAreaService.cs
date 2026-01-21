using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfacesServicios
{
    public interface IAreaService
    {
        Task<IEnumerable<AreaDTO>> GetAllAsync();
        Task<bool> UpdateAsync(AreaDTO dto);
        Task<int> CreateAsync(AreaDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
