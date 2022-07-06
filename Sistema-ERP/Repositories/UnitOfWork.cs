using Sistema_ERP.Interfaces;

namespace Sistema_ERP.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            ICategoriaRepository categoriaRepo,
            IProdutoRepository produtoRepo,
            IUsuarioRepository usuarioRepo)
        {
            Categorias = categoriaRepo;
            Produtos = produtoRepo;
            Usuarios = usuarioRepo;
        }      
        public IProdutoRepository Produtos { get; }
        public ICategoriaRepository Categorias { get; }
        public IUsuarioRepository Usuarios { get; }
    }
}
