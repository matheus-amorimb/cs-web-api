namespace ApiCatalogo.Repositories;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    
    Task CommitAsync();
}