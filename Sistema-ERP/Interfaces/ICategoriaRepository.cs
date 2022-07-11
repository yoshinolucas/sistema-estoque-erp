using Sistema_ERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema_ERP.Interfaces
{
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        Task<IReadOnlyList<Categoria>> GetByNomeSearchAsync(string nome);
        Task<IReadOnlyList<Categoria>> GetByNomeAsync();
        Task<IReadOnlyList<Categoria>> GetByNomeDescAsync();
    }
}
