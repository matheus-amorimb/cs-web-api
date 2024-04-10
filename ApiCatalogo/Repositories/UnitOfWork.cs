using ApiCatalogo.Context;

namespace ApiCatalogo.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProductRepository _productRepository;
    private ICategoryRepository _categoryRepository;
    public AppDbContext Context;
    
    public UnitOfWork(AppDbContext context)
    {
        Context = context;
    }
    
    public IProductRepository ProductRepository
    {
        get { return _productRepository = _productRepository ?? new ProductRepository(Context); }
    }
    public ICategoryRepository CategoryRepository
    {
        get { return _categoryRepository = _categoryRepository ?? new CategoryRepository(Context); }
    }

    public void Commit()
    {
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}