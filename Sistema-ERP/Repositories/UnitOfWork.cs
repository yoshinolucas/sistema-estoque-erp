using Sistema_ERP.Interfaces;

namespace Sistema_ERP.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ICategoriaRepository categoriaRepo, IProdutoRepository produtoRepo)
        {
            Categorias = categoriaRepo;
            Produtos = produtoRepo;
        }

        public IProdutoRepository Produtos { get; }
        public ICategoriaRepository Categorias { get; }
    }
}
