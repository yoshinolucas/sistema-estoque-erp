using Sistema_ERP.Models;

namespace Sistema_ERP.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(Usuario usuario);
        void RemoverSessaoDoUsuario();
        Usuario BuscarSessaoDoUsuario();
    }
}
