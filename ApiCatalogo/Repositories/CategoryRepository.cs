using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class CategoryRepository : ICategorieRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public Category GetCategory(int id)
    {
        return _context.Categories.Find(id);
    }

    public Category Create(Category category)
    {
        _context.Categories.Add(category); //incluir na memória
        _context.SaveChanges();

        return category;
    }

    public Category Update(Category category)
    {
        _context.Categories.Entry(category).State = EntityState.Modified; //já está na memória, é preciso modificar o estado
        _context.SaveChanges();

        return category;
    }

    public Category Delete(int id)
    {
        var category = _context.Categories.Find(id);
        _context.Categories.Remove(category);
        _context.SaveChanges();

        return category;
    }
}