using Sistema_ERP.Interfaces;

namespace Sistema_ERP.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ICategoriaRepository categoriaRepo)
        {
            Categorias = categoriaRepo;
        }

        public ICategoriaRepository Categorias { get; }
    }
}
