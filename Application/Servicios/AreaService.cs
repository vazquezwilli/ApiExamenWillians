using Application.Dtos;
using Application.InterfacesServicios;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Servicios
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _repository;
        private readonly IValidator<AreaDTO> _validator;

        public AreaService(IAreaRepository repository, IValidator<AreaDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<AreaDTO>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<int> CreateAsync(AreaDTO dto)
        {
          
            return await _repository.CreateAsync(dto);
        }

        public async Task<bool> UpdateAsync(AreaDTO dto)
        {
            return await _repository.UpdateAsync(dto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
