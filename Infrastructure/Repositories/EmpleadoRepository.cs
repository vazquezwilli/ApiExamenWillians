using Dapper;
using Application.Dtos;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly string _connectionString;

        public EmpleadoRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<EmpleadoDTO>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<EmpleadoDTO>(
                "sp_Empleado_GetAll",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> CreateAsync(EmpleadoDTO dto)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("Nombre", dto.Nombre);
            parameters.Add("Apellidos", dto.Apellidos);
            parameters.Add("FechaNacimiento", dto.FechaNacimiento);
            parameters.Add("Email", dto.Email);
            parameters.Add("AreaId", dto.AreaId);
            parameters.Add("FechaContratacion", dto.FechaContratacion);
            return await connection.ExecuteScalarAsync<int>(
                "sp_Empleado_Insert",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> UpdateAsync(EmpleadoDTO dto)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("EmpleadoId", dto.EmpleadoId);
            parameters.Add("Nombre", dto.Nombre);
            parameters.Add("Apellidos", dto.Apellidos);
            parameters.Add("FechaNacimiento", dto.FechaNacimiento);
            parameters.Add("Email", dto.Email);
            parameters.Add("AreaId", dto.AreaId);
            parameters.Add("FechaContratacion", dto.FechaContratacion);

            var rowsAffected = await connection.ExecuteAsync(
                "sp_Empleado_Update",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            var rowsAffected = await connection.ExecuteAsync(
                "sp_Empleado_Delete",
                new { EmpleadoId = id },
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected > 0;
        }
    }
}
