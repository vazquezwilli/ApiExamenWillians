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
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _repository;
        private readonly IValidator<EmpleadoDTO> _validator;

        public EmpleadoService(IEmpleadoRepository repository, IValidator<EmpleadoDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<EmpleadoDTO>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<int> CreateAsync(EmpleadoDTO dto)
        {
            return await _repository.CreateAsync(dto);
        }

        public async Task<bool> UpdateAsync(EmpleadoDTO dto)
        {
            return await _repository.UpdateAsync(dto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
