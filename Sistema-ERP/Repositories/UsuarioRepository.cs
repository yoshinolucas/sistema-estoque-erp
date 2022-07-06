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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration _configuration;

        public UsuarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> AddAsync(Usuario usuario)
        {
            usuario.Data_Criada = DateTime.Now;
            var sql = @"Insert into Usuarios (Nome, Login, Email, Senha, Perfil, Data_Criada)
                        VALUES (@Nome, @Login, @Email, @Senha, @Perfil, @Data_Criada)";
            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.ExecuteAsync(sql, usuario);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Usuarios WHERE Id_Usuario = @Id_Usuario";
            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.ExecuteAsync(sql, new { Id_Usuario = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Usuario>> GetAllAsync()
        {
            var sql = "SELECT * FROM Usuarios";
            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.QueryAsync<Usuario>(sql);
                return result.ToList();
            }
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Usuarios WHERE Id_Usuario = @Id_Usuario";
            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.QuerySingleOrDefaultAsync<Usuario>(sql, new { Id_Usuario = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Usuario usuario)
        {
            usuario.Data_Modificada = DateTime.Now;
            var sql = "UPDATE Usuarios SET Nome = @Nome, Email = @Email, Login = @Login, Perfil = @Perfil, Data_Modificada = @Data_Modificada  WHERE Id_Usuario = @Id_Usuario";
            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.ExecuteAsync(sql, usuario);
                return result;
            }
        }
    }
}
