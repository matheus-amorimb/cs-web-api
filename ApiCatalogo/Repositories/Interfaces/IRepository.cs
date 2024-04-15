using System.Linq.Expressions;

namespace ApiCatalogo.Repositories;

public interface IRepository<T>
{
    // IEnumerable<T> GetAll();
    //Async method
    Task<IEnumerable<T>> GetAllAsync();
    
    //Async method
    Task<T?> GetByIdAsync(Expression<Func<T, bool>> expression);
    // T? GetById(Expression<Func<T, bool>> expression);
    
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}