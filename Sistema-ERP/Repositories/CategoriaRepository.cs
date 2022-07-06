using Dapper;
using Microsoft.Extensions.Configuration;
using Sistema_ERP.Interfaces;
using Sistema_ERP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_ERP.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IConfiguration _configuration;

        public CategoriaRepository(IConfiguration configuration)
        {
            _configuration = configuration;           
        }

        public async Task<int> AddAsync(Categoria categoria)
        {
            categoria.Data_Criada = DateTime.Now;
            var sql = "Insert into Categorias (Nome,Data_Criada) Values (@Nome,@Data_Criada)";
            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.ExecuteAsync(sql, categoria);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Categorias WHERE Id_Categoria = @Id_Categoria";
            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.ExecuteAsync(sql, new { Id_Categoria = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Categoria>> GetAllAsync()
        {
            var sql = "SELECT * FROM Categorias";
            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.QueryAsync<Categoria>(sql);
                    return result.ToList();
            }
        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Categorias WHERE Id_Categoria = @Id_Categoria";
            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.QuerySingleOrDefaultAsync<Categoria>(sql, new { Id_Categoria = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Categoria categoria)
        {
            categoria.Data_Modificada = DateTime.Now;
            var sql = "UPDATE Categorias SET Nome = @Nome, Data_Modificada = @Data_Modificada WHERE Id_Categoria = @Id_Categoria";
            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.ExecuteAsync(sql, categoria);
                return result;
            }
        }
    }
}
