namespace Sistema_ERP.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoriaRepository Categorias { get; }
    }
}
