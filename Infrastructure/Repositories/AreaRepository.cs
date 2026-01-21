using Application.Dtos;
using Dapper;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories
{
    public  class AreaRepository: IAreaRepository
    {
        private readonly string _connectionString;

        public AreaRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<AreaDTO>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<AreaDTO>(
                "sp_Area_GetAll",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> CreateAsync(AreaDTO dto)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("Nombre", dto.Nombre);
            parameters.Add("Descripcion", dto.Descripcion);
            parameters.Add("Activo", dto.Activo);
            return await connection.ExecuteScalarAsync<int>(
                "sp_Area_Insert",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> UpdateAsync(AreaDTO dto)
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("AreaId", dto.AreaId);
            parameters.Add("Nombre", dto.Nombre);
            parameters.Add("Descripcion", dto.Descripcion);
            parameters.Add("Activo", dto.Activo);

            var rowsAffected = await connection.ExecuteAsync(
                "sp_Area_Update",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            var rowsAffected = await connection.ExecuteAsync(
                "sp_Area_Delete",
                new { AreaId = id },
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected > 0;
        }
    }
}
