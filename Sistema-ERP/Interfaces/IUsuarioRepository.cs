using Sistema_ERP.Models;
using System.Threading.Tasks;

namespace Sistema_ERP.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario> GetByLoginAsync(string login);
        Task<Usuario> GetByEmailLoginAsync(string email, string login);
        Task<int> UpdateSenhaAsync(Usuario entity);

    }
}
