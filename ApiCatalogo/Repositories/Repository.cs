using System.Linq.Expressions;
using ApiCatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext Context;
    
    public Repository(AppDbContext context)
    {
        Context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> expression)
    {
        return await Context.Set<T>().FirstOrDefaultAsync(expression);
    }

    public T Create(T entity)
    {
        Context.Set<T>().Add(entity);
        // Context.SaveChanges();
        return entity;

    }

    public T Update(T entity)
    {
        Context.Set<T>().Update(entity);
        // Context.SaveChanges();
        return entity;
        
        // Context.Entry(entity).State = EntityState.Modified;
        // Context.SaveChanges();
        // return entity;
    }

    public T Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
        // Context.SaveChanges();
        return entity;
    }
}