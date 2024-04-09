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

    public IEnumerable<T> GetAll()
    {
        return Context.Set<T>().ToList();
    }

    public T? GetById(Expression<Func<T, bool>> expression)
    {
        return Context.Set<T>().FirstOrDefault(expression);
    }

    public T Create(T entity)
    {
        Context.Set<T>().Add(entity);
        Context.SaveChanges();
        return entity;

    }

    public T Update(T entity)
    {
        Context.Set<T>().Update(entity);
        Context.SaveChanges();
        return entity;
        
        // Context.Entry(entity).State = EntityState.Modified;
        // Context.SaveChanges();
        // return entity;
    }

    public T Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
        Context.SaveChanges();
        return entity;
    }
}