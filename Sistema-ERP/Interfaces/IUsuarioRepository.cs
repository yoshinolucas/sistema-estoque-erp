using Sistema_ERP.Models;
using System.Threading.Tasks;

namespace Sistema_ERP.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario> GetByLoginAsync(string login);
    }
}
