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
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IConfiguration _configuration;

        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> AddAsync(Produto produto)
        {
            produto.Data_Criada = DateTime.Now;
            var sql = @"Insert into Produtos(Nome, Preco, Estoque, Id_Categoria, Data_Criada)
                        VALUES (@Nome, @Preco, @Estoque, @Id_Categoria, @Data_Criada)";
            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.ExecuteAsync(sql, produto);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Produtos WHERE Id_Produto = @Id_Produto";
            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.ExecuteAsync(sql, new { Id_Produto = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Produto>> GetAllAsync()
        {
            var sql = @"SELECT p.Id_Produto, p.Nome, p.Preco, p.Estoque, c.Id_Categoria, c.Nome
                        FROM Produtos AS p
                        INNER JOIN Categorias AS c
                        ON p.Id_Categoria = c.Id_Categoria";

            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.QueryAsync
                    <Produto, Categoria, Produto>
                    (sql,
                    (produto, categoria) =>
                    {
                        produto.Categoria = categoria;
                        return produto;
                    },
                    splitOn: "Id_Categoria");
                return result.ToList();
            }
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Produtos WHERE Id_Produto = @Id_Produto";
            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.QuerySingleOrDefaultAsync<Produto>(sql, new { Id_Produto = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Produto produto)
        {
            produto.Data_Modificada = DateTime.Now;
            var sql = @"Update Produtos SET
                        Nome = @Nome, Preco = @Preco, Estoque = @Estoque, Id_Categoria = @Id_Categoria, Data_Modificada = @Data_Modificada
                        WHERE Id_Produto = @Id_Produto";
            using (var _connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                _connection.Open();
                var result = await _connection.ExecuteAsync(sql, produto);
                return result;
            }
        }
    }
}
